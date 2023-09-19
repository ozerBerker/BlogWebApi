using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Domain
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected readonly IConfiguration configuration;
        public Context(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("WebApiDatabase"));
            //optionsBuilder.UseSqlServer("Server=DESKTOP-9C3JF0M;Database=BlogWebApi;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //Blog Tables
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
