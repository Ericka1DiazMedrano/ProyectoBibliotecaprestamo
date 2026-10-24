using Microsoft.EntityFrameworkCore;
using Biblioteca_prestamo.Data;
using Biblioteca_prestamo.Models;

namespace Biblioteca_prestamo.Services;

public class BibliotecaService
{
    private readonly BibliotecaDbContext _context;

    public BibliotecaService(BibliotecaDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    // --- LIBROS ---
    public void RegistrarLibro(string titulo, string autor, string isbn, int stock)
    {
        var libro = new Libro
        {
            Titulo = titulo,
            Autor = autor,
            ISBN = isbn,
            StockTotal = stock,
            StockDisponible = stock
        };
        _context.Libros.Add(libro);
        _context.SaveChanges();
    }

    public List<Libro> ObtenerLibros() => _context.Libros.ToList();

    // --- USUARIOS ---
    public void RegistrarUsuario(string nombre, string documento, string email)
    {
        var usuario = new Usuario
        {
            Nombre = nombre,
            Documento = documento,
            Email = email
        };
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<Usuario> ObtenerUsuarios() => _context.Usuarios.ToList();

    // --- PRÉSTAMOS ---
    public bool RegistrarPrestamo(int libroId, int usuarioId, int diasPrestamo)
    {
        var libro = _context.Libros.Find(libroId);
        var usuario = _context.Usuarios.Find(usuarioId);

        if (libro == null || usuario == null) return false;
        if (libro.StockDisponible <= 0) return false;

        libro.StockDisponible--;

        var prestamo = new Prestamo
        {
            LibroId = libroId,
            UsuarioId = usuarioId,
            FechaPrestamo = DateTime.Now,
            FechaDevolucionEsperada = DateTime.Now.AddDays(diasPrestamo),
            Activo = true
        };

        _context.Prestamos.Add(prestamo);
        _context.SaveChanges();
        return true;
    }

    public bool RegistrarDevolucion(int prestamoId)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.Libro)
            .FirstOrDefault(p => p.Id == prestamoId);

        if (prestamo == null || !prestamo.Activo) return false;

        prestamo.Activo = false;
        prestamo.FechaDevolucionReal = DateTime.Now;
        prestamo.Libro.StockDisponible++;

        _context.SaveChanges();
        return true;
    }

    public List<Prestamo> ObtenerPrestamosActivos()
    {
        return _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .Where(p => p.Activo)
            .ToList();
    }

    public List<Prestamo> ObtenerHistorialPrestamos()
    {
        return _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .ToList();
    }
}
