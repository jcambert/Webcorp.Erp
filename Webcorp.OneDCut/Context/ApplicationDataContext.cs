using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webcorp.OneDCut.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Webcorp.OneDCut.Context
{
    public class ApplicationDataContext : IdentityDbContext<AppUser>
    {
        public ApplicationDataContext()
            : base("DefaultConnection")
        { }

        public System.Data.Entity.DbSet<AppUser> AppUsers { get; set; }
    }
}