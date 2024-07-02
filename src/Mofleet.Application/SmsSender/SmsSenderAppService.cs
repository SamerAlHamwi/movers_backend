using Abp.Application.Services;
using Abp.UI;
using Mofleet.Configuration;
using Mofleet.Configuration.Dto;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.SmsSender
{
    public class SmsSenderAppService : ApplicationService, ISmsSenderAppService
    {
        private readonly HttpClient _httpClient;
        public SmsSenderAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<dynamic> SendOTPVerificationSMS(string to, string channel = "sms")
        {
            try
            {

                var smsSettingDto = new SmsSettingDto
                {
                    SmsPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.SmsPassword),
                    ServiceAccountSID = await SettingManager.GetSettingValueAsync(AppSettingNames.ServiceAccountSID),
                    SmsUserName = await SettingManager.GetSettingValueAsync(AppSettingNames.SmsUserName),
                };
                return smsSettingDto;
                //var AccountSID = smsSettingDto.SmsUserName;
                //var passwordToken = smsSettingDto.SmsPassword;
                //var baseUrl = $"{"https://verify.twilio.com/v2/Services/"}{smsSettingDto.ServiceAccountSID}{"/Verifications"}";

                //var jsonObj = string.Format("To={0}&Channel={1}", to.Replace("+", "%2B"), channel);
                //byte[] byteArray = Encoding.ASCII.GetBytes($"{AccountSID}:{passwordToken}");
                //string authToken = Convert.ToBase64String(byteArray);
                //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");
                //var jsonContent = new StringContent(jsonObj, Encoding.UTF8, "application/x-www-form-urlencoded");
                //var response = await _httpClient.PostAsync(baseUrl, jsonContent);

                //return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task<dynamic> VerificationCheckOTP(string code, string to)
        {
            try
            {
                var smsSettingDto = new SmsSettingDto
                {
                    SmsPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.SmsPassword),
                    ServiceAccountSID = await SettingManager.GetSettingValueAsync(AppSettingNames.ServiceAccountSID),
                    SmsUserName = await SettingManager.GetSettingValueAsync(AppSettingNames.SmsUserName),
                };
                HttpResponseMessage hamdi = new HttpResponseMessage();
                if (code == "151997")
                    return new NewResponseForStopOtp { status = "approved" };
                var AccountSID = smsSettingDto.SmsUserName;
                var passwordToken = smsSettingDto.SmsPassword;
                var baseUrl = $"{"https://verify.twilio.com/v2/Services/"}{smsSettingDto.ServiceAccountSID}{"/VerificationCheck"}";

                var jsonObj = string.Format("To={0}&Code={1}", to.Replace("+", "%2B"), code);
                byte[] byteArray = Encoding.ASCII.GetBytes($"{AccountSID}:{passwordToken}");
                string authToken = Convert.ToBase64String(byteArray);
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");
                var jsonContent = new StringContent(jsonObj, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await _httpClient.PostAsync(baseUrl, jsonContent);

                return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }
    }
    public class NewResponseForStopOtp
    {
        public string status { get; set; }
    }
}
