using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApplication3.Areas.Login.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoginModel result = DeserializeTicket();
                 ViewBag.userName = result.strName;
            }
            return View();


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private LoginModel DeserializeTicket() {

            HttpCookie cookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            var serializer = new JavaScriptSerializer();
            LoginModel result = serializer.Deserialize<LoginModel>(ticket.UserData);
            return result;
        }
    }
}