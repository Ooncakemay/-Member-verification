using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Areas.Login.Models;

namespace WebApplication3.Areas.Register.Models
{
    public class RegisterViewModel
    {
        /// <summary>
        /// E-Mail(帳號)
        /// </summary>
        public String strEMail { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public String strPassword { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        public String strName { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public String strMobile { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime dtmBirth { get; set; }

        /// <summary>
        /// 縣市
        /// </summary>
        public String strCity { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public int intZipCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public String strAddress { get; set; }



    }
}