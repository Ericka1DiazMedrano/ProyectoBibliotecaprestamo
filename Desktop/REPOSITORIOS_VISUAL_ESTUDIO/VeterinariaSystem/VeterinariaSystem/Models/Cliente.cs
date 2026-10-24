using System.ComponentModel.DataAnnotations;

namespace VeterinariaSystem.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Telefono { get; set; } = string.Empty;

    [EmailAddress]
    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public List<Mascota> Mascotas { get; set; } = new();
}