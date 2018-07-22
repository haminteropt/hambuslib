namespace CouchDB
{
    using HamBusLib.DataRepo;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CouchDBClient" />
    /// </summary>
    public class CouchDBClient : IGenericRepo
    {
        private string BaseCouchDbApiAddress { get; set; }
        private HttpClient Client { get; set; }

        private string AuthCouchDbCookieKeyName = "AuthSession";
        private CookieContainer CookieContainer = new CookieContainer();

        public CouchDBClient(string baseUrl)
        {
             BaseCouchDbApiAddress = baseUrl;
             Client = new HttpClient() { BaseAddress = new Uri(BaseCouchDbApiAddress) };
        }

        public CouchUser User { get; set; }
        public string AuthCookie { get; set; }
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        public Boolean Auth<T>(T couchDbCredentials) where T : class
        {
            AuthCookie = GetAuthenticationCookie(couchDbCredentials);
            if (AuthCookie == null)
                return false;
            CookieContainer.Add(new Uri(BaseCouchDbApiAddress), new Cookie(AuthCouchDbCookieKeyName, AuthCookie));

            return true;
        }
        public void CreateDataBase<T>(T dbName)
        {
            HttpContent content = new StringContent("{}", Encoding.UTF8, "application/json");
            CookieContainer.Add(new Uri(BaseCouchDbApiAddress), new Cookie(AuthCouchDbCookieKeyName, AuthCookie));
            var result = Client.PutAsync("/" + dbName,content).Result;
        }
        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        public void DropDataBase<C>(C dbEntity) where C : class
        {
            throw new NotImplementedException();
        }
        public List<T> GetAllDataBases<T>() where T : class
        {

            var result = Client.GetAsync("/_all_dbs").Result;
            if (result.IsSuccessStatusCode)
            {
                //Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                var list = JsonConvert.DeserializeObject<List<T>>(result.Content.ReadAsStringAsync().Result);
                return list;
            }
            else
            {
                Console.WriteLine(result.ReasonPhrase);
            }
            return null;
        }
        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }
        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        private string GetAuthenticationCookie<T>(T credentials)
        {
            string authPayload = JsonConvert.SerializeObject(credentials);
            var authResult = Client.PostAsync("/_session", new StringContent(authPayload, Encoding.UTF8, "application/json")).Result;
            if (authResult.IsSuccessStatusCode)
            {
                var responseHeaders = authResult.Headers.ToList();
                string plainResponseLoad = authResult.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Authenticated user from CouchDB API:");
                Console.WriteLine(plainResponseLoad);
                User = JsonConvert.DeserializeObject<CouchUser>(plainResponseLoad);


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
