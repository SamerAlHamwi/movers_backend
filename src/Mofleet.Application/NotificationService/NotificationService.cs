using Abp;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization.Users;
using Mofleet.EmailSender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Mofleet.NotificationService
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly ISettingManager _settingManager;
        private readonly IAbpSession _session;
        private readonly ILocalizationSource _localizationSource;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private IEmailSenderAppService _emailSenderAppService;

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly INotificationPublisher _notificationPublisher;
        //private readonly IDelegationManager _delegationManager;
        /// <summary>
        /// NotificationService
        /// </summary>
        /// <param name="notificationPublisher"></param>
        /// <param name="settingManager"></param>
        /// <param name="session"></param>
        /// <param name="configuration"></param>
        /// <param name="localizationManager"></param>
        /// <param name="userManager"></param>
        /// <param name="unitOfWorkManager"></param>

        public NotificationService(INotificationPublisher notificationPublisher,
            //IDelegationManager delegationManager,
            ISettingManager settingManager,
            IAbpSession session,
            IConfiguration configuration,
            ILocalizationManager localizationManager,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IEmailSenderAppService emailSenderAppService)
        {
            _notificationPublisher = notificationPublisher;
            //_delegationManager = delegationManager;
            _settingManager = settingManager;
            _session = session;
            _configuration = configuration;
            _localizationSource = localizationManager.GetSource(MofleetConsts.LocalizationSourceName);
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _emailSenderAppService = emailSenderAppService;
        }

        //public async Task NotifyUserAndHisDelegationUsersAsync(TypedMessageNotificationData data, long userId)
        //{
        //    var userIds = (await _delegationManager.GetEffectiveDelegationsFromUserAsync(userId))
        //        .Select(x => x.DelegatedToUserId).ToList();
        //    userIds.Add(userId);

        //    await NotifyUsersAsync(data, userIds.ToArray());
        //}

        //public async Task NotifyUsersAndTheirDelegationUsersAsync(TypedMessageNotificationData data, List<long> userIds)
        //{
        //    var notifiedUserIds = userIds.ToList();

        //    foreach (var userId in userIds)
        //    {
        //        notifiedUserIds.AddRange((await _delegationManager.GetEffectiveDelegationsFromUserAsync(userId))
        //            .Select(x => x.DelegatedToUserId).ToList());
        //    }

        //    await NotifyUsersAsync(data, notifiedUserIds.Distinct().ToArray());
        //}
        /// <summary>
        /// Notify Users
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task NotifyUsersAsync(TypedMessageNotificationData data, long[] userIds, bool withNotify, bool forEmailToo = false)
        {
            var userIdentifiers = userIds.Select(x =>
                new UserIdentifier(MultiTenancyConsts.DefaultTenantId, x)).ToArray();

            var notificationName = data.NotificationType.ToString();

            await _notificationPublisher.PublishAsync(notificationName, data, userIds: userIdentifiers);

            await SendPushNotification(data, userIds, withNotify: true, forEmailToo);

        }


        private async Task SendPushNotification(TypedMessageNotificationData data, long[] userIds, bool withNotify, bool forEmailToo = false)
        {
            var fcmApiUri = new Uri(_configuration["FCM:ApiUrl"]);
            var fcmAppId = _configuration["FCM:AppId"];
            var fcmSenderId = _configuration["FCM:SenderId"];
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var users = await _userManager.Users.AsNoTracking().Where(x => userIds.Contains(x.Id)).Select(x => new { x.Id, x.FcmToken, x.EmailAddress, x.IsEmailConfirmed }).ToListAsync();
                foreach (var userId in userIds)
                {
                    string result = null;
                    var user = users.Where(x => x.Id == userId).FirstOrDefault();
                    if (user is null)
                        continue;
                    var lang = await _settingManager.GetSettingValueForUserAsync(LocalizationSettingNames.DefaultLanguage, _session.TenantId, userId);
                    var isArabic = lang.ToUpper().Contains("AR");
                    var title = _localizationSource.GetString(data.NotificationType.ToString(), isArabic ?
                        CultureInfo.GetCultureInfo("ar") :
                        CultureInfo.GetCultureInfo("en"));
                    var notificationProperties = data.Properties;
                    var message = isArabic ? data.ArMessage : data.EnMessage;
                    if (forEmailToo && user.IsEmailConfirmed)
                        await _emailSenderAppService.SendEmail(
                           user.EmailAddress,
                   $"{title}\n{message}",
                   ConfirmationCodeType.ConfirmEmail,
                   true
                            );
                    if (string.IsNullOrEmpty(user.FcmToken))
                        continue;


                    var extData = new Dictionary<string, string>
                            {
                                {"time", DateTime.Now.ToString("dd-MM-yyyy HH:mm")},
                                {"type", ((byte) data.NotificationType).ToString()},
                            };
                    if (withNotify)
                    {
                        var postData = new
                        {
                            to = user.FcmToken,
                            notification = new
                            {
                                title = title,
                                body = message,
                                sound = "Enabled",
                                priority = "high",
                            },
                            data = extData
                        };
                        try
                        {
                            var webRequest = WebRequest.Create(fcmApiUri);
                            webRequest.Method = "POST";
                            webRequest.Headers.Add($"Authorization: key={fcmAppId}");
                            webRequest.Headers.Add($"Sender: id={fcmSenderId}");
                            webRequest.ContentType = "application/json";

                            var jsonMessage = JsonConvert.SerializeObject(postData);

                            var byteArray = Encoding.UTF8.GetBytes(jsonMessage);

                            webRequest.ContentLength = byteArray.Length;

                            using var dataStream = await webRequest.GetRequestStreamAsync();
                            await dataStream.WriteAsync(byteArray, 0, byteArray.Length);

                            using var webResponse = await webRequest.GetResponseAsync();
                            using var dataStreamResponse = webResponse.GetResponseStream();
                            using var tReader = new StreamReader(dataStreamResponse);
                            result = await tReader.ReadToEndAsync();
                        }
                        catch
                        {
                            Logger.Error($"PushNotification not sent to ({result}).");
                        }
                    }
                    if (!withNotify)
                    {
                        var postData = new
                        {
                            to = user.FcmToken,
                            content_available = true,
                            priority = "high",
                            time_to_live = 120, // 2 mins
                            data = extData
                        };
                        try
                        {
                            var webRequest = WebRequest.Create(fcmApiUri);
                            webRequest.Method = "POST";
                            webRequest.Headers.Add($"Authorization: key={fcmAppId}");
                            webRequest.Headers.Add($"Sender: id={fcmSenderId}");
                            webRequest.ContentType = "application/json";

                            var jsonMessage = JsonConvert.SerializeObject(postData);

                            var byteArray = Encoding.UTF8.GetBytes(jsonMessage);

                            webRequest.ContentLength = byteArray.Length;

                            using var dataStream = await webRequest.GetRequestStreamAsync();
                            await dataStream.WriteAsync(byteArray, 0, byteArray.Length);

                            using var webResponse = await webRequest.GetResponseAsync();
                            using var dataStreamResponse = webResponse.GetResponseStream();
                            using var tReader = new StreamReader(dataStreamResponse);
                            result = await tReader.ReadToEndAsync();
                        }
                        catch
                        {
                            Logger.Error($"PushNotification not sent to ({result}).");
                        }
                    }
                }
            }
        }
    }
}
