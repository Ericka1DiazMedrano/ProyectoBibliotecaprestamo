using Microsoft.EntityFrameworkCore;
using VeterinariaSystem.Data;
using VeterinariaSystem.Models;

namespace VeterinariaSystem.Services;

public class VeterinariaService
{
    private readonly VeterinariaDbContext _db;

    // CONSTRUCTOR: Se ejecuta automáticamente al instanciar el servicio
    public VeterinariaService()
    {
        _db = new VeterinariaDbContext();
        _db.Database.EnsureCreated();

        // Inserta un veterinario por defecto si la tabla está vacía
        if (!_db.Veterinarios.Any())
        {
            _db.Veterinarios.Add(new Veterinario
            {
                Nombre = "Dr. Veterinario Principal",
                Especialidad = "Medicina General",
                Telefono = "00000000"
            });
            _db.SaveChanges();
        }
    }

    // --- CRUD CLIENTES ---
    public void GuardarCliente(Cliente cliente)
    {
        if (cliente.Id == 0)
            _db.Clientes.Add(cliente);
        else
            _db.Clientes.Update(cliente);

        _db.SaveChanges();
    }

    public List<Cliente> ObtenerClientes() => _db.Clientes.ToList();

    public void EliminarCliente(int id)
    {
        var cliente = _db.Clientes.Find(id);
        if (cliente != null)
        {
            _db.Clientes.Remove(cliente);
            _db.SaveChanges();
        }
    }

    // --- CRUD MASCOTAS ---
    public void GuardarMascota(Mascota mascota)
    {
        if (mascota.Id == 0)
            _db.Mascotas.Add(mascota);
        else
            _db.Mascotas.Update(mascota);

        _db.SaveChanges();
    }

    public List<Mascota> ObtenerMascotasConPropietario()
    {
        return _db.Mascotas
            .Include(m => m.Cliente)
            .ToList();
    }

    public void EliminarMascota(int id)
    {
        var mascota = _db.Mascotas.Find(id);
        if (mascota != null)
        {
            _db.Mascotas.Remove(mascota);
            _db.SaveChanges();
        }
    }

    // --- CRUD CONSULTAS ---
    public void RegistrarConsulta(Consulta consulta)
    {
        _db.Consultas.Add(consulta);
        _db.SaveChanges();
    }

    public List<Consulta> ObtenerHistorialClinico()
    {
        return _db.Consultas
            .Include(c => c.Mascota)
            .Include(c => c.Veterinario)
            .OrderByDescending(c => c.Fecha)
            .ToList();
    }
}