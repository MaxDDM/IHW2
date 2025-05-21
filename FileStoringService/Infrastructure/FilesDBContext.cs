using FileStoringService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FileStoringService.Infrastructure
{
    public class FilesDBContext : DbContext
    {
        public DbSet<FileText> Files { get; set; } = null!;

        public FilesDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=postgresql_db;Username=postgres;Password=1234");
        }
    }
}
