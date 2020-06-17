using ClientApp.Models;
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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                if (Session["jwt"]!= null)
                {
                    //return RedirectToAction("Index", "Attendance");
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> GetLogin(string username, string password)
        {

            try
            {
                User usr = new User();
                usr.UserName = username;
                usr.Password = password;
                HttpResponseMessage result;
                using (LoginService _repo = new LoginService())
                {

                    result = await _repo.LoginAndGetJwt(usr, "api/Auth/AuthenticateAndGenerateToken");
                }
                if (result.IsSuccessStatusCode)
                {
                    string data = await result.Content.ReadAsStringAsync();
                    Session["jwt"] = JsonConvert.DeserializeObject<string>(data);                  
                        
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ResultLogin"] = "Invalid UserName or Password !";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ResultLogin"] = "Invalid UserName or Password !";
                return View("Index");
            }
        }
    }
}