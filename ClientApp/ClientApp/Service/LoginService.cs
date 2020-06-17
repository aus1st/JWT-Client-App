using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using ClientApp.Models;
using System.Text;

namespace ClientApp.Service
{
	public class LoginService: IDisposable
	{


        public async Task<HttpResponseMessage> LoginAndGetJwt(User model, string Url)
        {

            using (var client = new HttpClient())
            {
                //setup client

                client.BaseAddress = new Uri("http://localhost:xxxxx/"); //use your api url
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string req = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = client.PostAsync(Url, new StringContent(req, Encoding.UTF8, "application/json")).Result;

                return response;
            }


        }


        public async Task<HttpResponseMessage> GetValues(string jwt, string Url)
        {

            using (var client = new HttpClient())
            {
                //setup client

                client.BaseAddress = new Uri("http://localhost:xxxxx/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                jwt);

                HttpResponseMessage response = client.GetAsync(Url).Result;
                return response;
            }

        }
    

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

    }

}