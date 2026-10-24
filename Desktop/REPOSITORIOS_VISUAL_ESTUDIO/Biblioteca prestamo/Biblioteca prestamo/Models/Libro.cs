using System.ComponentModel.DataAnnotations;

namespace Biblioteca_prestamo.Models;

public class Libro
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Autor { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string ISBN { get; set; } = string.Empty;

    public int StockTotal { get; set; }
    public int StockDisponible { get; set; }
}