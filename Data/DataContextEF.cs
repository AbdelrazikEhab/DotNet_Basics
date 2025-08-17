using System.Data;
using Dapper;
using DotNetAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<UserSalary> UserSalary { set; get; }
        public virtual DbSet<UserJobInfo> UserJobInfo { set; get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                 optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>()
            .ToTable("Users", "TutorialAppSchema")
            .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
         .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
         .HasKey(u => u.UserId);
        }


    }
}