using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.DBContext
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext()
            : base("DBEntity")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<AppUser>()
                .ToTable("Users");
        }
    }
}