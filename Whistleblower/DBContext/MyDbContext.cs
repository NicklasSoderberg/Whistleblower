using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.DBContext
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        // Other part of codes still same 
        // You don't need to add AppUser and AppRole 
        // since automatically added by inheriting form IdentityDbContext<AppUser>

        public DbSet<LoginModel> LoginModel { get; set; }
    }  
}