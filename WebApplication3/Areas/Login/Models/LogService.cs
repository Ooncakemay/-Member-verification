using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.Areas.Register.Models;

namespace WebApplication3.Areas.Login.Models
{
    public class LogService
    {
        private FAQEntities db = new FAQEntities();
        public void Register(Log_MemberAccessSys se_memberLog)
        {
            db.Log_MemberAccessSys.Add(se_memberLog);
            db.SaveChanges();
        }
        public void InsertLogInsertLogMember(string strMemberID, string strLogType, string strLogWHO,RegisterViewModel register)
        {
            Log_Member newLog = new Log_Member();
            newLog.strMemberID = strMemberID;
            newLog.strEMail = register.strEMail;
            newLog.strPassword = register.strPassword;
            newLog.strName = register.strName;
            newLog.strPhone = "";
            newLog.strMobile = register.strMobile;
            newLog.strWHO = strLogWHO;
            newLog.strPhoneCity = register.strCity;
            newLog.strDisplayName = register.strName;
            newLog.strLogType = strLogType;
            newLog.strLogWHO = strLogWHO;
            newLog.dtmUpdate = DateTime.Now;
            newLog.dtmCreate = DateTime.Now;
            newLog.dtmLogDate = DateTime.Now;
            db.Log_Member.Add(newLog);
            db.SaveChanges();
        }
        public bool CheckMailExisted(String strMemberID) {
            bool isExist = false;
            var selectMemberID = db.Log_EmailSent.FirstOrDefault(u => u.strMemberID.Equals(strMemberID));

            if (selectMemberID != null)
            {
                isExist = true;
            }
            return isExist;

        }
        public void InsertLogEmailSent(String strMemberID,string strEMAIL) {
            Log_EmailSent emailSent = new Log_EmailSent();
            emailSent.strMemberID = strMemberID;
            emailSent.strEMAIL = strEMAIL;
            emailSent.dtmSendDate = DateTime.Now;
            emailSent.dtmCreate = DateTime.Now;
            emailSent.intSendCnt = 0;
            emailSent.ysnSend = "N";
            emailSent.ysnActivate = false;
            emailSent.ysnSendDone = false;

            db.Log_EmailSent.Add(emailSent);
            db.SaveChanges();
        }
        public void UpdateLogEmailSend(string strMemberID, int AddCount, int ChangeActivated)
        {
            var logEmail = db.Log_EmailSent.FirstOrDefault(u => u.strMemberID.Equals(strMemberID) );

            if (AddCount != 0) {
                logEmail.intSendCnt += AddCount;
            }
            if (ChangeActivated != 0)
            {
                logEmail.ysnActivate = true;
                logEmail.ysnSendDone = true;
            }
            db.SaveChanges();

        }
    }
}