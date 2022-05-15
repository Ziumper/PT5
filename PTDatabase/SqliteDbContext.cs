using Microsoft.EntityFrameworkCore;
using PTDatabase.Models;
using System.Reflection;

namespace PTDatabase
{
    public class SqliteDbContext : DbContext
    {
        public static readonly string FileName = "fileExplorer.db";
        public static readonly string Path =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + '\\' + FileName;

        public DbSet<User> Users { get; set; }
        public DbSet<Models.File> Files { get; set; }
        public DbSet<FileHistory> History { get; set; }
        public DbSet<FilePermission> Permissions { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        {
            builder.UseSqlite("Filename=" + Path, option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<FileHistory>().ToTable("FilesHistory");
            modelBuilder.Entity<Models.File>().ToTable("Files");
            modelBuilder.Entity<FilePermission>().ToTable("FilesPermissions");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Login).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
