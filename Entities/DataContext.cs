using System;
using Microsoft.EntityFrameworkCore; 

namespace WebApi.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }

        private string GetConnectionString()
        {
            const string server = "localhost";
            const string databaseName = "dotnet_test";
            const string userId = "sa";
            const string databasePassword = "masterkey";
            const string serverPort = "2000";

            return $"Server={server};" +
                    $"database={databaseName};" +
                    $"User Id={userId};" +
                    $"Password={databasePassword};" +
                    $"Port={serverPort};" +
                    $"Integrated Security=true;" +
                    $"pooling=true;";

        }

        public DbSet<Entities.Employee> Employees { get; set; }
        public DbSet<Entities.AuthUser> AuthUsers { get; set; }
        public DbSet<Entities.Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Employee>().ToTable("employees");
            modelBuilder.Entity<AuthUser>().ToTable("auth_users");
           
        }
    }
}