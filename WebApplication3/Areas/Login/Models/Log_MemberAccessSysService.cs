using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Areas.Login.Models
{
    public class Log_MemberAccessSysService
    {
        private FAQEntities db = new FAQEntities();
        public void Register(Log_MemberAccessSys se_memberLog)
        {
            db.Log_MemberAccessSys.Add(se_memberLog);
            db.SaveChanges();
        }
      


    }
}