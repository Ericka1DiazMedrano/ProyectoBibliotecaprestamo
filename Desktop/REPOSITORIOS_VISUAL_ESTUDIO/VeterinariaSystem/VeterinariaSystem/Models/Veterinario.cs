using System.ComponentModel.DataAnnotations;

namespace VeterinariaSystem.Models;

public class Veterinario
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Especialidad { get; set; } = string.Empty;

    public string? Telefono { get; set; }
}