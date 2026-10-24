using Microsoft.EntityFrameworkCore;
using VeterinariaSystem.Models;

namespace VeterinariaSystem.Data;

public class VeterinariaDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Veterinario> Veterinarios { get; set; }
    public DbSet<Consulta> Consultas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=veterinaria_v2.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mascota>()
            .HasOne(m => m.Cliente)
            .WithMany(c => c.Mascotas)
            .HasForeignKey(m => m.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Mascota)
            .WithMany(m => m.Consultas)
            .HasForeignKey(c => c.MascotaId);

        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Veterinario)
            .WithMany()
            .HasForeignKey(c => c.VeterinarioId);
    }
}