using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moustique.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Context
{
    public class MoustiqueContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public MoustiqueContext(DbContextOptions<MoustiqueContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" });
            base.OnModelCreating(builder);
        }

        public DbSet<Statistics> Statistics { get; set; }
    }
}
