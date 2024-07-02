using Abp.Application.Services;
using Abp.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mofleet.Configuration;
using Mofleet.Configuration.Dto;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.EmailSender
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EmailSenderAppService : ApplicationService, IEmailSenderAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailSenderAppService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task SendEmail(string email, string code, ConfirmationCodeType codeType, bool usingWithNotification = false)
        {

            var emailSettingDto = new EmailSettingDto()
            {
                SenderEmail = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderEmail),
                SenderPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderPassword),
                SenderHost = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderHost),
                SenderPort = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.SenderPort),
                SenderEnableSsl = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderEnableSsl),
                SenderUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderUseDefaultCredentials),
                Message = await SettingManager.GetSettingValueAsync(AppSettingNames.Message),
                MessageForResetPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.MessageForResetPassword)
            };
            try
            {
                //var enMessage = LocalizationSource.GetString("PushNotification", CultureInfo.GetCultureInfo("en"));


                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailSettingDto.SenderEmail);
                mail.Subject = "Go Movaro";
                if (codeType == ConfirmationCodeType.ConfirmEmail)
                    mail.Body = $"{emailSettingDto.Message}\n{code}";

                else
                    mail.Body = $"{emailSettingDto.MessageForResetPassword}\n{code}";
                if (usingWithNotification)
                    mail.Body = code;
                mail.IsBodyHtml = true;
                mail.To.Add(email);

                SmtpClient smtp = new SmtpClient(emailSettingDto.SenderHost, emailSettingDto.SenderPort);
                smtp.EnableSsl = emailSettingDto.SenderEnableSsl;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential
                {
                    UserName = emailSettingDto.SenderEmail,
                    Password = emailSettingDto.SenderPassword
                };


                smtp.UseDefaultCredentials = emailSettingDto.SenderUseDefaultCredentials;
                smtp.Credentials = NetworkCred;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }


    }
}
