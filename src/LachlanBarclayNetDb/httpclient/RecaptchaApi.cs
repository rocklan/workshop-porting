using System;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace LachlanBarclayNet.Controllers
{
    public class RecaptchaApi
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly string _recaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";
        private readonly string _secret = ConfigurationManager.AppSettings["recaptchaSecretKey"];

        public async Task<RecaptureResult> RecaptchaIsOkAsync(string recaptchaToken, string remoteip)
        {
            if (recaptchaToken == null)
                return new RecaptureResult("Token was empty");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["secret"] = _secret;
            query["response"] = recaptchaToken;
            query["remoteip"] = remoteip;
            string queryStringEncoded = query.ToString();

            string recaptureUrl = $"{_recaptchaUrl}?{queryStringEncoded}";
            var httpResponse = await _httpClient.PostAsync(recaptureUrl, null);

            if (!httpResponse.IsSuccessStatusCode)
                return new RecaptureResult($"Request to recaptcha api failed: {httpResponse.StatusCode}");

            var txtResponse = await httpResponse.Content.ReadAsStringAsync();

            try
            {
                
                var objectResponse = JsonConvert.DeserializeObject<RecaptureResponseDTO>(txtResponse);

                if (!objectResponse.Success)
                    return new RecaptureResult(string.Join(",", objectResponse.ErrorCodes));

                if (!string.Equals(objectResponse.Action, "contact"))
                    return new RecaptureResult("Invalid action: " + objectResponse.Action);

                if (objectResponse.Score < 0.5m)
                    return new RecaptureResult("You are most likely a bot, please contact me on twitter!");

                return new RecaptureResult
                {
                    BotScore = objectResponse.Score,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new RecaptureResult(e.Message);
            }
        }

    

        private class RecaptureResponseDTO
        {
            public decimal Score { get; set; }
            public string Action { get; set; }
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public string[] ErrorCodes { get; set; }
        }
    }


    public class RecaptureResult
    {
        public RecaptureResult() { }
        public RecaptureResult(string Errors)
        {
            this.Errors = Errors;
            this.Success = false;
        }
        public bool Success { get; set; }
        public decimal BotScore { get; set; }
        public string Errors { get; set; }
    }



}
