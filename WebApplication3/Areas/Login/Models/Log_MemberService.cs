using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Areas.Login.Models
{
    public class Log_MemberService
    {
        FAQEntities db = new FAQEntities();
        public void InsertLogInsertLogMember(string strMemberID, string strLogType, string strLogWHO)
        {
            Log_Member newLog = new Log_Member();
            newLog.strMemberID = strMemberID;
            newLog.strLogType = strLogType;
            newLog.strLogWHO = strLogWHO;
            newLog.dtmUpdate = DateTime.Now;
            newLog.dtmLogDate = DateTime.Now;
            db.Log_Member.Add(newLog);
            db.SaveChanges();
        }
    }
}