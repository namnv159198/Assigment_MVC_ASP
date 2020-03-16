using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASSIGMENT_MVC_ASP.Data
{
    public class ASSIGMENT_MVC_ASPContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ASSIGMENT_MVC_ASPContext() : base("name=ASSIGMENT_MVC_ASPContext")
        {
        }

        public System.Data.Entity.DbSet<ASSIGMENT_MVC_ASP.Models.SanPham> SanPhams { get; set; }

        public System.Data.Entity.DbSet<ASSIGMENT_MVC_ASP.Models.LoaiSanPham> LoaiSanPhams { get; set; }
    }
}
