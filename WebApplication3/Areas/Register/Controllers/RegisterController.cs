using System;
using System.Configuration;
using System.Web.Mvc;
using WebApplication3.Areas.Login.Models;
using WebApplication3.Areas.Register.Models;
using WebApplication3.SendNow;
using NLog;
namespace WebApplication3.Areas.Register.Controllers
{
    public class RegisterController : Controller
    {
        private MemberService memberService = new MemberService();
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult RegisterMember(RegisterViewModel registerData)
        {
            bool IsInsert = false;
            MailService mailSercice = new MailService();
            string strActivateCode = mailSercice.GenerateEmailToken();
            string strMemberID =  memberService.RegisterNewMember(registerData, strActivateCode);
        
            if (strMemberID != "") {

                IsInsert = true;
                logService.InsertLogInsertLogMember(strMemberID, "I", "TECRM", registerData);
                
                if (!logService.CheckMailExisted(strMemberID))
                {
                    logService.InsertLogEmailSent(strMemberID, registerData.strEMail);
                }

                if (SendVerifyMail(registerData.strEMail, registerData.strName, strMemberID, strActivateCode))
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
        public ActionResult EmailValidate(string strMemberID, string strActivateCode) {
            if (memberService.CheckStrActivateCode(strMemberID,strActivateCode)) {
                if (memberService.UpdateActive(strMemberID)) {
                    ViewBag.Validate = "success";
                }
            }
            return View();
        }

       

   
        public ActionResult Final() {
 
            return View();
        }
        public Boolean SendVerifyMail(String email, String name, String strMemberID, String strActivateCode)
        {
            string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
            string project_category_code = ConfigurationManager.AppSettings["ProjectCategoryCode"];
            string toname = name;
            string toemail = email;
            string fromname = ConfigurationManager.AppSettings["EmailFromName"];
            string fromemail = ConfigurationManager.AppSettings["EmailFrom"];
            string subject = name + "你好，理膚寶水-敏感肌膚美好生活會員email驗證通知函";
            string content = string.Format("<div style=\"width: 665px\">" +
                                                "<div style=\"padding-bottom:10px; font-size: small\">" +
                                                "</div><div style=\"font-size: medium\">" +

                                                "親愛的" + name + " 您好，<br /><br />" +
                                                "歡迎您加入理膚寶水-敏感肌膚美好生活，為確認您的email帳號無誤。<br />" +
                                                "請由此處完成您的會員帳號認證：<a href=\"" + WebDomain + "/Register/Register/EmailValidate?strMemberID=" + strMemberID + "&strActivateCode=" + strActivateCode + "\">請點選此處進行會員帳號啟用</a>。完成會員申請。<br />" +
                                                "若您並未進行註冊申請，請忽略本封郵件。謝謝！<br /><br />" +
                                                "若點選連結卻無法完成驗證程序時，請直接<a href=\"" + WebDomain + "/Register/ContactUs/\">聯絡我們</a>，將盡快為您處理！<br /><br />" +
                                                "理膚寶水-敏感肌膚美好生活會員小組<br />" +
                                                "敬上</div>" +
                                                "<div style=\"border-style: dashed; border-color: #000; border-width: 1px; border-left-width: 0px; border-right-width: 0px; padding: 10px 0px 10px 0px; margin: 10px 0px 10px 0px; font-size: small\">" +
                                                "歡迎您隨時登入理膚寶水-敏感肌膚美好生活查詢個人積點歷程及更多會員好禮兌換活動資訊。<br />" +
                                                "為了保障您的會員權益，請牢記您的網站登入帳號及密碼，並請勿告訴他人。</div>" +
                                                "<div style=\"font-size: small\"><br />" +
                                                "<div style=\"color: gray;\">" +
                                                "此信件為系統發出信件，請勿直接回覆，感謝您的配合。謝謝！<br />" +
                                                "如果您有任何問題，歡迎<a href=\"" + WebDomain + "/Register/ContactUs/\">聯絡我們</a>或撥打客服電話，將有專人為您服務<br />" +
                                                "理膚寶水-敏感肌膚美好生活：<a href=\"" + WebDomain + "\">www.lrp.com.tw</a><br /></div>" +
                                                "會員服務中心：0800-257-889</div></div>");

            // 寄信Function
            try
            {
                SendNowSoapClient objAPIService = new SendNowSoapClient();
                Boolean bResult = objAPIService.SendToOne(project_category_code, toname, toemail, fromname, fromemail, subject, content);


                return true;
            }
            catch (Exception e)
            {
                logger.Error("SendVerifyMail " + e.Message);  // Error log
                return false;
            }
        }//End SendVerifyMail

        //public bool SendVerifyMail(String strEMail, String strName, String strMemberID)
        //{
        //    var result = false;
        //    MailService mailService = new MailService();
        //    string authCode = mailService.GenerateEmailToken();
        //    string context = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Register/Views/Register/RegisterEmailTemplate.html"));
        //    UriBuilder VaildateUrl = new UriBuilder(Request.Url)
        //    {
        //        Path = Url.Action("vaildateUrl","Register", new{ Areas="",UserName = strName,AuthCode = authCode })
        //    };
        //    string mailBody = mailService.GetRegisterBody(context, strName, authCode);
         
        //        mailService.SendRegisterMail(mailBody, strEMail);
        //        TempData["registerResult"] = "註冊成功請去收信";
        //        result = true;
            
          
        //    return result;

        //}
    }
}