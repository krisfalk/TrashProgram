﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MunicipalTrashProgram.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string PickupDay { get; set; }
        public DateTime DateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        [ForeignKey("UserInfo")]
        public int? UserInfo_id { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        [ForeignKey("Worker")]
        public int? Worker_id { get; set; }
        public virtual Worker Worker { get; set; }
        [ForeignKey("Address")]
        public int? Address_id { get; set; }
        public virtual Address Address { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
       // public DbSet<Person> people { get; set; }
        public DbSet<Worker> workers { get; set; }
        public DbSet<UserInfo> usersInfo { get; set; }
        public DbSet<Address> addresses { get; set; }
    }
}