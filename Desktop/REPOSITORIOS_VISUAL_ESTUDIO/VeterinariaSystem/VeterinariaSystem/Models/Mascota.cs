using System.ComponentModel.DataAnnotations;

namespace VeterinariaSystem.Models;

public class Mascota
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Especie { get; set; } = string.Empty;

    public string? Raza { get; set; }
    public int Edad { get; set; }
    public double Peso { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public List<Consulta> Consultas { get; set; } = new();
}