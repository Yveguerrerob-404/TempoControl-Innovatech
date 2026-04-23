using TempoControl.Domain;
using TempoControl.Data;
using TempoControl.Business;

var repo = new EmpleadoRepository();
repo.InicializarBaseDeDatos();

bool mostrarMenu = true;

while (mostrarMenu)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("INNOVATECH SOLUTIONS, S.R.L");
    Console.WriteLine("     SISTEMA TEMPOCONTROL");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Registrar Nuevo Empleado");
    Console.WriteLine("2. Registrar Entrada");
    Console.WriteLine("3. Registrar Salida");
    Console.WriteLine("4. Generar Reporte Mensual");
    Console.WriteLine("5. Gestión de Empleados");
    Console.WriteLine("6. Salir");
    Console.WriteLine("----------------------------------------");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine() ?? "";

    if (opcion == "1")
    {
        var nuevoEmpleado = new Empleado();
        Console.WriteLine("\n--- Registro de Empleado ---");
        Console.Write("Ingrese ID: ");
        nuevoEmpleado.Id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nombre Completo: ");
        nuevoEmpleado.NombreCompleto = Console.ReadLine() ?? "";
        Console.Write("Departamento: ");
        nuevoEmpleado.Departamento = Console.ReadLine() ?? "";
        Console.Write("Posición: ");
        nuevoEmpleado.Posicion = Console.ReadLine() ?? "";

        repo.CrearEmpleado(nuevoEmpleado);
        Console.WriteLine("\n¡Empleado guardado exitosamente!");
        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
        Console.ReadKey();
    }
    else if (opcion == "2")
    {
        Console.Write("\nIngrese ID para registrar Entrada: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            repo.RegistrarEntrada(id);
            Console.WriteLine("Entrada capturada a las: " + DateTime.Now);
        }
        else { Console.WriteLine("ID no válido."); }
        
        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
        Console.ReadKey();
    }
    else if (opcion == "3")
{
    Console.Write("\nIngrese ID para registrar Salida: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        repo.RegistrarSalida(id);
        string horaSalida = DateTime.Now.ToString("hh:mm:ss tt"); 
        Console.WriteLine($"\n¡Salida guardada exitosamente a las {horaSalida}!");
    }
    else 
    { 
        Console.WriteLine("ID no válido."); 
    }

    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
    Console.ReadKey();
}
  else if (opcion == "4")
{
    Console.Write("\nIngrese el ID para generar reporte mensual: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        string nombreEmpleado = repo.ObtenerNombrePorId(id);

        if (string.IsNullOrEmpty(nombreEmpleado))
        {
            Console.WriteLine("\n[!] El empleado con ese ID no existe.");
        }
        else
        {
            var listaDeHoras = repo.ObtenerHorasDeEmpleado(id); 
            var servicioCalculos = new CalculosService();
            double totalMes = servicioCalculos.SumarHorasDelMes(listaDeHoras);

            Console.WriteLine("\n========================================");
            Console.WriteLine("       REPORTE: INNOVATECH SOLUTIONS, S.R.L");
            Console.WriteLine("========================================");
            
            Console.WriteLine($"Nombre del Empleado: {nombreEmpleado}"); 
            Console.WriteLine($"Total de días trabajados: {listaDeHoras.Count}");
            Console.WriteLine($"Total de horas en el mes: {totalMes:F2} hrs"); 
            
            Console.WriteLine("========================================");
        }
    }
    else 
    {
        Console.WriteLine("ID no válido.");
    }

    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
    Console.ReadKey();
}
else if (opcion == "5")
{
    bool seguirEnMantenimiento = true;

    while (seguirEnMantenimiento)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("GESTIÓN DE EMPLEADOS");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Ver Lista de Todos");
        Console.WriteLine("2. Modificar Datos");
        Console.WriteLine("3. Dar de Baja");
        Console.WriteLine("4. Volver al menú principal");
        Console.WriteLine("========================================");
        Console.Write("\nSeleccione una acción: ");
        
        string mant = Console.ReadLine() ?? "";

        if (mant == "1") 
        {
            Console.WriteLine("\nID   | NOMBRE               | DEPTO           | STATUS");
            Console.WriteLine("--------------------------------------------------------");
            var lista = repo.ObtenerTodos();
            foreach(var e in lista) 
            {
                string statusTexto = (e.Activo == 1) ? "Activo" : "De Baja";
                Console.WriteLine($"{e.Id:D4} | {e.NombreCompleto,-20} | {e.Departamento,-15} | {statusTexto}");
            }
            Console.WriteLine("\nPresione una tecla para continuar gestionando...");
            Console.ReadKey();
        }
        else if (mant == "2") 
        {
            Console.Write("\nID a modificar: "); 
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Nuevo Depto: "); string d = Console.ReadLine() ?? "";
                Console.Write("Nueva Posición: "); string p = Console.ReadLine() ?? "";
                repo.ActualizarEmpleado(id, d, p);
                Console.WriteLine("\n[!] Datos actualizados correctamente.");
            }
            else { Console.WriteLine("ID no válido."); }
            Thread.Sleep(1500); 
        }
        else if (mant == "3") 
        {
            Console.Write("\nID para desactivar: "); 
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                repo.DesactivarEmpleado(id);
                Console.WriteLine("\n[!] Empleado desactivado en el sistema.");
            }
            else { Console.WriteLine("ID no válido."); }
            Thread.Sleep(1500);
        }
        else if (mant == "4") 
        {
            seguirEnMantenimiento = false; 
        }
    }
}
      else if (opcion == "6")
    {
        Console.WriteLine("\nGracias por usar el sistema de INNOVATECH SOLUTIONS, S.R.L.");
        Console.WriteLine("Hasta luego.");
        mostrarMenu = false;
    }
    
    else
    {
        Console.WriteLine("\nOpción no válida.");
        System.Threading.Thread.Sleep(1000);
    }
}