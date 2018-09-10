using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using NLog;
using System.Web.Security;
using System.Web.Script.Serialization;
using WebApplication3.Areas.Login.Models;
using WebApplication3.Areas.Register.Models;
using System.Security.Cryptography;
using System.Data.Entity.Validation;

namespace WebApplication3.Areas.Login.Models
{
    public class MemberService
    {
        private FAQEntities db = new FAQEntities();
        public void Register(SE_Member member) {
            db.SE_Member.Add(member);
            db.SaveChanges();
        }
        [HttpPost]
        public string LoginCheck(string Account, string password) {
            SE_Member loginMember = db.SE_Member.FirstOrDefault(u => u.strEMail == Account);
            string status = "";

            if (loginMember != null)
            {
           
                if (!PasswordCheck(loginMember, password))
                {
                    status = "WrongPassword";
                }
                else
                {
                    if (!loginMember.ysnActive)
                    {
                        status = "notActive";
                    }
                    else
                    {
                        if (!loginMember.ysnActivate)
                        {

                            status = "notActivate";
                        }
                        else
                        {
                            status = "Success";
                        }
                    }
                }
            }
            else {
                status = "NoAccount";
            }
            return status;


        }
        protected bool PasswordCheck(SE_Member checkMember,string Password) {
            bool result = checkMember.strPassword.Equals(Password);
            return result;
        }
        public String GetUserData(string strEMail, string strCookieID) {
            SE_Member loginMember = db.SE_Member.FirstOrDefault(u => u.strEMail == strEMail);
            LoginModel loginInfo = new LoginModel();
            loginInfo.strMemberID = loginMember.strMemberID;
            loginInfo.strName = loginMember.strName;
            loginInfo.strCookieID = strCookieID;

            String UserData = new JavaScriptSerializer().Serialize(loginInfo);

            return UserData;
        }
        public string GetStrMemberID(string strEMail)
        {
            string strMembverID = "not found user";
            SE_Member se = db.SE_Member.FirstOrDefault(u => u.strEMail == strEMail);
            //防呆
            if (se != null) {
                strMembverID = se.strMemberID;
            }
            return strMembverID; 
        }
   
        public bool AccountCheck(RegisterViewModel register) {
            bool isExist = false;
            var selectEmail = db.SE_Member.FirstOrDefault(u => u.strEMail == register.strEMail);
            
            if (selectEmail != null) {
                isExist = true;
            }
            return isExist;
            
        }
        public bool RegisterNewMember(RegisterViewModel register) {
            SE_Member newMember = new SE_Member();
   
            bool isInsert = false;

            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[8];
                provider.GetBytes(data);
                newMember.strMemberID = Convert.ToBase64String(data);
            }
            newMember.strEMail = register.strEMail;
            newMember.strName = register.strName;
            newMember.strPhone = "";
            newMember.strPassword = register.strPassword;
            newMember.dtmBirth = register.dtmBirth;
            newMember.intZipCode = register.intZipCode;
            newMember.strAddress = register.strAddress;
            newMember.strMobile = register.strMobile;
            newMember.dtmCreate = DateTime.Now;
            newMember.ysnPolicy = false;
            newMember.ysnDM = false;
            newMember.ysnSMS = false;
            newMember.ysnTel = false;
            newMember.dtmUpdate = DateTime.Now;



            newMember.strWHO = "FAQ";
            newMember.strDisplayName = "";

            try
            {
                db.SE_Member.Add(newMember);
                db.SaveChanges();
                isInsert = true;
            }
            catch (DbEntityValidationException ex)
            { 
                throw ex;
            }
            return isInsert;
        }


    }
}