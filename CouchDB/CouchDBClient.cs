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
        /// <summary>
        /// Defines the BaseCouchDbApiAddress
        /// </summary>
        private static string BaseCouchDbApiAddress = "http://localhost:5984";

        /// <summary>
        /// Defines the Client
        /// </summary>
        private HttpClient Client = new HttpClient() { BaseAddress = new Uri(BaseCouchDbApiAddress) };

        /// <summary>
        /// Defines the AuthCouchDbCookieKeyName
        /// </summary>
        private string AuthCouchDbCookieKeyName = "AuthSession";

        /// <summary>
        /// Defines the CookieContainer
        /// </summary>
        private static CookieContainer CookieContainer = new CookieContainer();

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public CouchUser User { get; set; }

        /// <summary>
        /// Gets or sets the AuthCookie
        /// </summary>
        public string AuthCookie { get; set; }

        /// <summary>
        /// The Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity<see cref="T"/></param>
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Auth
        ///                     public Task<bool> Auth<T>(T auth) where T : class
        /// </summary>
        /// <param name="couchDbCredentials">The couchDbCredentials<see cref="Credentials"/></param>
        public Boolean Auth<T>(T couchDbCredentials) where T : class

        {
            AuthCookie = GetAuthenticationCookie(couchDbCredentials);
            if (AuthCookie != null)
                return true;
            else return false;
        }

        /// <summary>
        /// The CreateDataBase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbEntity">The dbEntity<see cref="T"/></param>
        public void CreateDataBase<T>(T dbEntity) where T : class
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// The Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity<see cref="T"/></param>
        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The DropDataBase
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <param name="dbEntity">The dbEntity<see cref="C"/></param>
        public void DropDataBase<C>(C dbEntity) where C : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetAllDataBases
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="List{T}"/></returns>
        public List<T> GetAllDataBases<T>() where T : class
        {
            CookieContainer.Add(new Uri(BaseCouchDbApiAddress), new Cookie(AuthCouchDbCookieKeyName, AuthCookie));
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

        /// <summary>
        /// The SaveAll
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity<see cref="T"/></param>
        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetAuthenticationCookie
        /// </summary>
        /// <param name="credentials">The credentials<see cref="Credentials"/></param>
        /// <returns>The <see cref="string"/></returns>
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
