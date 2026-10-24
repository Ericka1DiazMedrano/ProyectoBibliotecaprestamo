namespace Biblioteca_prestamo.Models;

public class Prestamo
{
    public int Id { get; set; }

    public int LibroId { get; set; }
    public Libro Libro { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public DateTime FechaPrestamo { get; set; } = DateTime.Now;
    public DateTime FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }

    public bool Activo { get; set; } = true;
}