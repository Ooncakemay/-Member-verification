using System;
using System.Web.Mvc;
using WebApplication3.Areas.Login.Models;
using WebApplication3.Areas.Register.Models;

namespace WebApplication3.Areas.Register.Controllers
{
    public class RegisterController : Controller
    {
        private MemberService memberService = new MemberService();
        LogService logService = new LogService();
        // GET: Register/Register

        public ActionResult index() {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Register() {
            return View();
        }

        public ActionResult RegisterMember(RegisterViewModel registerData)
        {
            

            bool IsInsert =  memberService.RegisterNewMember(registerData);
            if (IsInsert) {
                string strMemberID = strMemberID = memberService.GetStrMemberID(registerData.strEMail);
                logService.InsertLogInsertLogMember(strMemberID, "I", "TECRM", registerData);
                if (logService.CheckMailExisted(strMemberID))
                {
                    logService.InsertLogEmailSent(strMemberID, registerData.strEMail);
                }

                if (SendVerifyMail(registerData.strEMail, registerData.strName, strMemberID))
                {
                    logService.UpdateLogEmailSend(strMemberID, 1, 0);
                }
                else
                {
                    logService.UpdateLogEmailSend(strMemberID, 0, 0);
                }
            }

            return Json(IsInsert, JsonRequestBehavior.AllowGet);
        }

      

        [HttpPost]
        public ActionResult IsRegister(RegisterViewModel registerData) {
            return Json(memberService.AccountCheck(registerData), JsonRequestBehavior.AllowGet);
        }

   
        public ActionResult Final() {
  

            return View();
        }


        public bool SendVerifyMail(String strEMail, String strName, String strMemberID)
        {
            var result = false;
            MailService mailService = new MailService();
            string authCode = mailService.GenerateEmailToken();
            string context = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Register/Views/Register/RegisterEmailTemplate.html"));
            UriBuilder VaildateUrl = new UriBuilder(Request.Url)
            {
                Path = Url.Action("vaildateUrl","Register", new{ Areas="",UserName = strName,AuthCode = authCode })
            };
            string mailBody = mailService.GetRegisterBody(context, strName, authCode);
         
                mailService.SendRegisterMail(mailBody, strEMail);
                TempData["registerResult"] = "註冊成功請去收信";
                result = true;
            
          
            return result;

        }
    }
}