using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Uploaders.Helpers
{
    public class Imgur
    {
        public class ImgurTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("scope")]
            public object Scope { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("account_id")]
            public int AccountID { get; set; }

            [JsonProperty("account_username")]
            public string AccountUsername { get; set; }
        }

        public string ClientID { get; set; } = null;
        public string ClientSecret { get; set; } = null;

        private static readonly WebClient client = new WebClient();

        public async Task<ImgurTokenResponse> GetTokens(string pin)
        {
            Contract.Requires(pin != null);
            Contract.Requires(ClientID != null);
            Contract.Requires(ClientSecret != null);

            string url = "https://api.imgur.com/oauth2/token";

            var values = new System.Collections.Specialized.NameValueCollection
            {
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "grant_type", "pin" },
                { "pin", pin }
            };
            
            byte[] bytes = await client.UploadValuesTaskAsync(url, values);
            string str = Encoding.ASCII.GetString(bytes);

            return JsonConvert.DeserializeObject<ImgurTokenResponse>(str);
        }
    }
}
