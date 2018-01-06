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
            const string databaseName = "ngnl_dev";
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

        public DbSet<Entities.employee> employees { get; set; }
        public DbSet<Entities.auth_user> auth_users { get; set; }
        public DbSet<Entities.auth_user_role> auth_user_roles { get; set; }
        public DbSet<Entities.Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<employee>().ToTable("employees");
           
        }
    }
}