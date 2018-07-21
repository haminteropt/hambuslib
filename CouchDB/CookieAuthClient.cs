using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HamBusLib.CouchDB
{
    public class Credentials
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
    public class CookieAuthClient
    {
        private static string BaseCouchDbApiAddress = "http://localhost:5984";
        private  HttpClient Client = new HttpClient() { BaseAddress = new Uri(BaseCouchDbApiAddress) };
        private  string AuthCouchDbCookieKeyName = "AuthSession";
        public void Auth()
        {
            Credentials couchDbCredentials = new Credentials()
            {
                Username = "olivia",
                Password = "secret"
            };

            string authCookie = GetAuthenticationCookie(couchDbCredentials);
        }
        private  string GetAuthenticationCookie(Credentials credentials)
        {
            string authPayload = JsonConvert.SerializeObject(credentials);
            var authResult = Client.PostAsync("/_session", new StringContent(authPayload, Encoding.UTF8, "application/json")).Result;
            if (authResult.IsSuccessStatusCode)
            {
                var responseHeaders = authResult.Headers.ToList();
                string plainResponseLoad = authResult.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Authenticated user from CouchDB API:");
                Console.WriteLine(plainResponseLoad);
                var authCookie = responseHeaders.Where(r => r.Key == "Set-Cookie").Select(r => r.Value.ElementAt(0)).FirstOrDefault();
                if (authCookie != null)
                {
                    int cookieValueStart = authCookie.IndexOf("=") + 1;
                    int cookieValueEnd = authCookie.IndexOf(";");
                    int cookieLength = cookieValueEnd - cookieValueStart;
                    string authCookieValue = authCookie.Substring(cookieValueStart, cookieLength);
                    return authCookieValue;
                }
                throw new Exception("There is auth cookie header in the response from the CouchDB API");
            }

            throw new HttpRequestException(string.Concat("Authentication failure: ", authResult.ReasonPhrase));
        }

    }
}
