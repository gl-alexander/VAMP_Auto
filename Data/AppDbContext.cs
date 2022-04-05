using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using VAMP_Auto.Models;


namespace VAMP_Auto.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Query> Queries { set; get; }
        public AppDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=vamp;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>()
                .HasKey(c => new { c.UserId, c.CarId });

            modelBuilder.Entity<Query>()
                .HasOne(q=>q.Car)
                .WithMany(q2 => q2.Queries)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<Query>()
                .HasOne(q=>q.User)
                .WithMany(q3 => q3.Queries)
                .HasForeignKey(c => c.UserId);
        }
    }
}
