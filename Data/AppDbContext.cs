using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VAMP_Auto.Models;


namespace VAMP_Auto.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Car> Cars { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Query> Queries { set; get; }
        public AppDbContext():base()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>()
                .HasKey(c => new { c.UserId, c.CarId });

            modelBuilder.Entity<User>()
                .HasMany(c => c.Queries)
                .WithRequired()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Queries)
                .WithRequired()
                .HasForeignKey(c => c.CarId);
        }
    }
}
