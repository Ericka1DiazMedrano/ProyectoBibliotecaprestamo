using Biblioteca_prestamo.Data;
using Biblioteca_prestamo.Services;

namespace Biblioteca_prestamo;

class Program
{
    static void Main(string[] args)
    {
        using var dbContext = new BibliotecaDbContext();
        var service = new BibliotecaService(dbContext);

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("  SISTEMA DE PRÉSTAMOS DE BIBLIOTECA   ");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Registrar usuario");
            Console.WriteLine("4. Listar usuarios");
            Console.WriteLine("5. Realizar préstamo");
            Console.WriteLine("6. Registrar devolución");
            Console.WriteLine("7. Ver préstamos activos");
            Console.WriteLine("8. Ver historial de préstamos");
            Console.WriteLine("9. Salir");
            Console.WriteLine("=======================================");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine() ?? "";
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine() ?? "";
                    Console.Write("ISBN: ");
                    string isbn = Console.ReadLine() ?? "";
                    Console.Write("Stock total: ");
                    int.TryParse(Console.ReadLine(), out int stock);

                    service.RegistrarLibro(titulo, autor, isbn, stock);
                    Console.WriteLine("\n¡Libro registrado exitosamente!");
                    Pausar();
                    break;

                case "2":
                    Console.WriteLine("\n--- LIBROS REGISTRADOS ---");
                    foreach (var l in service.ObtenerLibros())
                    {
                        Console.WriteLine($"[ID: {l.Id}] {l.Titulo} - {l.Autor} | ISBN: {l.ISBN} | Disp: {l.StockDisponible}/{l.StockTotal}");
                    }
                    Pausar();
                    break;

                case "3":
                    Console.Write("Nombre completo: ");
                    string nombre = Console.ReadLine() ?? "";
                    Console.Write("Documento ID: ");
                    string doc = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";

                    service.RegistrarUsuario(nombre, doc, email);
                    Console.WriteLine("\n¡Usuario registrado exitosamente!");
                    Pausar();
                    break;

                case "4":
                    Console.WriteLine("\n--- USUARIOS REGISTRADOS ---");
                    foreach (var u in service.ObtenerUsuarios())
                    {
                        Console.WriteLine($"[ID: {u.Id}] {u.Nombre} | Doc: {u.Documento} | Email: {u.Email}");
                    }
                    Pausar();
                    break;

                case "5":
                    Console.Write("ID del Libro: ");
                    int.TryParse(Console.ReadLine(), out int libroId);
                    Console.Write("ID del Usuario: ");
                    int.TryParse(Console.ReadLine(), out int usuarioId);
                    Console.Write("Días de préstamo: ");
                    int.TryParse(Console.ReadLine(), out int dias);

                    if (service.RegistrarPrestamo(libroId, usuarioId, dias))
                        Console.WriteLine("\n¡Préstamo registrado exitosamente!");
                    else
                        Console.WriteLine("\nError: ID inválido o stock insuficiente.");
                    Pausar();
                    break;

                case "6":
                    Console.Write("ID del Préstamo a devolver: ");
                    int.TryParse(Console.ReadLine(), out int prestamoId);

                    if (service.RegistrarDevolucion(prestamoId))
                        Console.WriteLine("\n¡Devolución completada!");
                    else
                        Console.WriteLine("\nError: No se encontró un préstamo activo con dicho ID.");
                    Pausar();
                    break;

                case "7":
                    Console.WriteLine("\n--- PRÉSTAMOS ACTIVOS ---");
                    foreach (var p in service.ObtenerPrestamosActivos())
                    {
                        Console.WriteLine($"[ID: {p.Id}] Libro: '{p.Libro.Titulo}' | Usuario: '{p.Usuario.Nombre}' | F. Devolución: {p.FechaDevolucionEsperada:dd/MM/yyyy}");
                    }
                    Pausar();
                    break;

                case "8":
                    Console.WriteLine("\n--- HISTORIAL DE PRÉSTAMOS ---");
                    foreach (var p in service.ObtenerHistorialPrestamos())
                    {
                        string estado = p.Activo ? "ACTIVO" : $"DEVUELTO ({p.FechaDevolucionReal:dd/MM/yyyy})";
                        Console.WriteLine($"[ID: {p.Id}] Libro: {p.Libro.Titulo} | Usuario: {p.Usuario.Nombre} | Estado: {estado}");
                    }
                    Pausar();
                    break;

                case "9":
                    salir = true;
                    break;
            }
        }
    }

    static void Pausar()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}