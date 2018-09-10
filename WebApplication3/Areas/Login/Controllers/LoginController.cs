using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Areas.Login.Models;


namespace WebApplication3.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        private MemberService memberService = new MemberService();


        // GET: Login/Login
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpPost]
        public ActionResult Login(string Account, string password)
        {
            string ValidateStr = memberService.LoginCheck(Account, password);
            String strCookieID = System.Guid.NewGuid().ToString("D");///***亂數產生器
            String lastUrl = "";
            bool ysnSuccess = false;

            if (ValidateStr.Equals("Success"))
            {
                //Ticket
                String userData = memberService.GetUserData(Account, strCookieID);
                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                    1,
                    memberService.GetStrMemberID(Account),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,//true 如果簽發持久 cookie(cookie 儲存在瀏覽器工作階段之間)。否則， false。
                    userData,
                    FormsAuthentication.FormsCookiePath);
                /* https://msdn.microsoft.com/zh-tw/library/system.web.security.formsauthenticationticket_properties(v=vs.110).aspx 
                 * http://yu0410aries.blogspot.com/2018/03/formsauthenticationticket.html
                   1,                                      //票證的版本琥碼
                    uid,                                    //與票證相關的使用者名稱
                    DateTime.Now,                           //核發此票證時的本機日期和時間
                    DateTime.Now.AddMinutes(60),            //票證到期的本機日期和時間
                    false,                                   //如果票證將存放於持續性Cookie中，則為true
                    "",                                     //要與票證一同存放的使用者特定資料
                    FormsAuthentication.FormsCookiePath);   //票證存放於Cookie中時的路徑
                 */

                string HashTicket = FormsAuthentication.Encrypt(Ticket);
                HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                Response.Cookies.Add(UserCookie);
                ///LogRegister 登入紀錄
                ysnSuccess = true;  
                lastUrl = GetLastUrl();
            }
            else
            {
                ysnSuccess = false;
            }
            LogRegister(Account, strCookieID, ysnSuccess);
            return Json(new { status = ValidateStr, LastUrl = lastUrl }, JsonRequestBehavior.AllowGet);
        }
        protected void LogRegister(string strEMail, string strCookieID, bool ysnSuccess)
        {
            Log_MemberAccessSysService log_MemberAccessService = new Log_MemberAccessSysService();
            Log_MemberAccessSys accessLog = new Log_MemberAccessSys();
            accessLog.strMemberID = memberService.GetStrMemberID(strEMail);//解決這一條(?
            accessLog.strIP = Request.ServerVariables["REMOTE_ADDR"];   // Get IP Address
            accessLog.strCookieID = strCookieID;
            accessLog.strType = "Login";
            accessLog.ysnSuccess = ysnSuccess;
            accessLog.dtmLogin = DateTime.Now;
            log_MemberAccessService.Register(accessLog);
        }
        protected string GetLastUrl()
        {
            String LastUrl = "";
            if (Session["LastUrl"] != null)
            {
                LastUrl = Session["LastUrl"].ToString();
                Session["LastUrl"] = null;
            }
            return LastUrl;
        }
    }
}