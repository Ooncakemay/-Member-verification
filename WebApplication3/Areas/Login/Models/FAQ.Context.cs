﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Areas.Login.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FAQEntities : DbContext
    {
        public FAQEntities()
            : base("name=FAQEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BS_Zip> BS_Zip { get; set; }
        public virtual DbSet<Log_EmailSent> Log_EmailSent { get; set; }
        public virtual DbSet<Log_Member> Log_Member { get; set; }
        public virtual DbSet<Log_MemberAccessSys> Log_MemberAccessSys { get; set; }
        public virtual DbSet<SE_Member> SE_Member { get; set; }
    }
}
