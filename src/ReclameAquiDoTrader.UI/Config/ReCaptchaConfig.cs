using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Config
{
    public class ReCaptchaConfig
    {
        public string Site_Key { get; set; }
        public string Secret_Key { get; set; }
    }

    public class GoogleReCaptchaService
    {
        private readonly ReCaptchaConfig _reCaptchaConfig;

        public GoogleReCaptchaService(IOptions<ReCaptchaConfig> reCaptchaConfig)
        {
            _reCaptchaConfig = reCaptchaConfig.Value;
        }

        public virtual async Task<GoogleReCaptchaResponse> VerificaReCaptcha(string token)
        {
            GoogleReCaptchaRequest reCaptchaRequest = new GoogleReCaptchaRequest()
            {
                Response = token,
                Secret = _reCaptchaConfig.Secret_Key
            };

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaRequest.Secret}&response={reCaptchaRequest.Response}");

            return JsonConvert.DeserializeObject<GoogleReCaptchaResponse>(response);
        }

    }


    public class GoogleReCaptchaRequest
    {
        public string Response { get; set; }
        public string Secret { get; set; }
    }

    public class GoogleReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Sucesso { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
        [JsonProperty("error-codes")]
        public List<string> Erros { get; set; }
    }

    public class ValidarReCaptchaRequest
    {
        public string Token { get; set; }
    }
}
