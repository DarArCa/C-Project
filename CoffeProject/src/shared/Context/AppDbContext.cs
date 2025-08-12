using CoffeProject.modules.Panel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeProject.shared.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany()
            .HasForeignKey(u => u.RoleId); 
            
        }
    }
}
