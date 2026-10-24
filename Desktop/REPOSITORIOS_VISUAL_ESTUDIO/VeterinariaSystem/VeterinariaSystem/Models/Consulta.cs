namespace VeterinariaSystem.Models;

public class Consulta
{
    public int Id { get; set; }

    public int MascotaId { get; set; }
    public Mascota Mascota { get; set; } = null!;

    public int VeterinarioId { get; set; }
    public Veterinario Veterinario { get; set; } = null!;

    public DateTime Fecha { get; set; } = DateTime.Now;
    public string Motivo { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
    public decimal Costo { get; set; }
}