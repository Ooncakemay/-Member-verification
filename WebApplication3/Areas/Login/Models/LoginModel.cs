using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Areas.Login.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 會員代碼
        /// </summary>
        public String strMemberID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public String strName { get; set; }

        /// <summary>
        /// Cookie id
        /// </summary>
        public String strCookieID { get; set; }
        /*
        /// <summary>
        /// 會員會籍是否為生效
        /// </summary>
        public Boolean ysnActive { get; set; }

        /// <summary>
        /// 帳號是否已開通
        /// </summary>
        public Boolean ysnActivate { get; set; }
         * 
        /// <summary>
        /// 帳號
        /// </summary> 
        public String strEMail { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public String strPassword { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public String strGender { get; set; }
        */
    }
}