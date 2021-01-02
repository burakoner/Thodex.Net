using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Thodex.Net.Helpers;

namespace Thodex.Net.CoreObjects
{
    public class ThodexAuthenticationProvider : AuthenticationProvider
    {
        private string tonce = "";
        private readonly ArrayParametersSerialization arraySerialization;

        public ThodexAuthenticationProvider(ApiCredentials credentials, ArrayParametersSerialization arraySerialization) : base(credentials)
        {
            if (credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            this.arraySerialization = arraySerialization;
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (!signed)
                return parameters;

            if (Credentials.Key == null || Credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            this.tonce = DateTime.UtcNow.ToUnixTimeSeconds().ToString();
            if (parameters == null) parameters = new Dictionary<string, object>();
            parameters.Add("apikey", Credentials.Key.GetString());
            parameters.Add("tonce", this.tonce);

            return parameters;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (Credentials.Key == null || Credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            if (uri.Contains("/v1/"))
            {
                var apiKey = Credentials.Key.GetString();
                var apiSecret = Credentials.Secret.GetString();
                var signparams = new SortedDictionary<string, object>(parameters);
                var signstring = string.Join("&", signparams.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value.ToString())}")) + $"&secret={apiSecret}";
                var signature = ThodexAuthenticationProvider.SHA256Hash(signstring);

                return new Dictionary<string, string> {
                    { "Authorization", signature },
                };
            }

            return new Dictionary<string, string>();
        }

        private int GetUnixTimestamp()
        {
            return System.Convert.ToInt32((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        }

        public static string SHA256Hash(string rawData)
        { 
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) sb.Append(bytes[i].ToString("x2"));
                return sb.ToString();
            }
        }
    }
}