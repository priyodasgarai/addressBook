using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
      //  public DbSet<Profile> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            /**********User Role*************************/
            List<IdentityRole> roles =
                new List<IdentityRole> {
                    new IdentityRole {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole {
                        Name = "User",
                        NormalizedName = "USER" },
                    new IdentityRole {
                        Name = "Store",
                        NormalizedName = "STORE"
                    }
                };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}