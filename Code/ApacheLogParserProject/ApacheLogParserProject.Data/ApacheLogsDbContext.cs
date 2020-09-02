using ApacheLogParserProject.Data.Configuration;
using ApacheLogParserProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApacheLogParserProject.Data
{
    public class ApacheLogsDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LogsDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfiguration());
        }
    }
}