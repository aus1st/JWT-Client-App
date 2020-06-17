using ClientApp.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["jwt"] != null)
            {
                ViewBag.Jwt = Session["jwt"].ToString();
                return View();
            }
            else {
                return RedirectToAction("Login","Index");
            }            
        }


       
       

        public async Task<ActionResult> About()
        {
            HttpResponseMessage result;
            using (LoginService _repo = new LoginService())
            {
                result = await _repo.GetValues(Session["jwt"].ToString(), "api/Values");
            }
            if (result.IsSuccessStatusCode)
            {
                string data = await result.Content.ReadAsStringAsync();
                string[] vals = JsonConvert.DeserializeObject<string[]>(data);

                ViewBag.Message = vals;
                return View();
            }
            else
            {
                ViewBag.Message = "You are not Authorized";
                return View();
            }

            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}