using Biblioteca_prestamo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Biblioteca_prestamo.Data;

public class BibliotecaDbContext : DbContext
{
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=biblioteca.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Libro)
            .WithMany()
            .HasForeignKey(p => p.LibroId);

        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Prestamos)
            .HasForeignKey(p => p.UsuarioId);
    }
}