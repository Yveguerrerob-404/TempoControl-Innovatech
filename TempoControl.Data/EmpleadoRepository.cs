using Microsoft.Data.Sqlite;
using TempoControl.Domain;

namespace TempoControl.Data;

public class EmpleadoRepository {
    private const string connectionString = "Data Source=tempocontrol.db"; 

    // para crear las tablas en la base de datos si no existen 
    public void InicializarBaseDeDatos() {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        
      // Creamos la tabla de Empleados y la de Fichajes 
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Empleados (
                Id INTEGER PRIMARY KEY, 
                Nombre TEXT, 
                Depto TEXT, 
                Posicion TEXT, 
                Activo INTEGER DEFAULT 1
            );
            CREATE TABLE IF NOT EXISTS Fichajes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EmpleadoId INTEGER,
                Entrada TEXT,
                Salida TEXT,
                FOREIGN KEY(EmpleadoId) REFERENCES Empleados(Id)
            );";
        command.ExecuteNonQuery();
    }

    // Para guardar un nuevo empleado en la base de datos 
    public void CrearEmpleado(Empleado emp) {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Empleados (Id, Nombre, Depto, Posicion, Activo) VALUES (@id, @nom, @dep, @pos, 1)";
        cmd.Parameters.AddWithValue("@id", emp.Id);
        cmd.Parameters.AddWithValue("@nom", emp.NombreCompleto);
        cmd.Parameters.AddWithValue("@dep", emp.Departamento);
        cmd.Parameters.AddWithValue("@pos", emp.Posicion);
        cmd.ExecuteNonQuery();
    }

   // Para desactivar un empleado (Borrado logico) 
    public void DesactivarEmpleado(int id) {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
       // Solo cambiamos el estado a 0, no lo borramos de la tabla 
        cmd.CommandText = "UPDATE Empleados SET Activo = 0 WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
public List<Empleado> ObtenerTodos()
{
    var lista = new List<Empleado>();
    using var conn = new SqliteConnection(connectionString);
    conn.Open();
    var cmd = conn.CreateCommand();
    cmd.CommandText = "SELECT Id, Nombre, Depto, Posicion, Activo FROM Empleados";
    
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        lista.Add(new Empleado {
            Id = reader.GetInt32(0),
            NombreCompleto = reader.GetString(1),
            Departamento = reader.GetString(2),
            Posicion = reader.GetString(3),
            Activo = reader.GetInt32(4)
        });
    }
    return lista;
}
public void ActualizarEmpleado(int id, string nuevoDepto, string nuevaPos)
{
    using var conn = new SqliteConnection(connectionString);
    conn.Open();
    var cmd = conn.CreateCommand();
    cmd.CommandText = "UPDATE Empleados SET Depto = @dep, Posicion = @pos WHERE Id = @id";
    cmd.Parameters.AddWithValue("@dep", nuevoDepto);
    cmd.Parameters.AddWithValue("@pos", nuevaPos);
    cmd.Parameters.AddWithValue("@id", id);
    cmd.ExecuteNonQuery();
}
    // Para registrar Hora de Entrada
public void RegistrarEntrada(int empleadoId) {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();
    var cmd = connection.CreateCommand();
    
    // Guardamos el ID del empleado y la fecha/hora exacta de este momento 
    cmd.CommandText = "INSERT INTO Fichajes (EmpleadoId, Entrada) VALUES (@id, @hora)";
    cmd.Parameters.AddWithValue("@id", empleadoId);
    cmd.Parameters.AddWithValue("@hora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    cmd.ExecuteNonQuery();
}

// Para registrar Hora de Salida
public void RegistrarSalida(int empleadoId) {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();
    var cmd = connection.CreateCommand();
    
    // Buscamos el registro de entrada de hoy que todavia no tenga salida
    cmd.CommandText = @"UPDATE Fichajes 
                        SET Salida = @hora 
                        WHERE EmpleadoId = @id AND Salida IS NULL";
    cmd.Parameters.AddWithValue("@id", empleadoId);
    cmd.Parameters.AddWithValue("@hora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    cmd.ExecuteNonQuery();
}

    public List<double> ObtenerHorasDeEmpleado(int id)
    {
        var horas = new List<double>();
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            
            // Busca los registros que ya tienen entrada y salida
            cmd.CommandText = "SELECT Entrada, Salida FROM Fichajes WHERE EmpleadoId = @id AND Salida IS NOT NULL";
            cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime entrada = DateTime.Parse(reader.GetString(0));
                    DateTime salida = DateTime.Parse(reader.GetString(1));
                    
                    // Calculamos la diferencia
                    double total = (salida - entrada).TotalHours;
                    horas.Add(total);
                }
            }
        }
        return horas;
    }

    public string ObtenerNombrePorId(int id)
{
    using var conn = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
    conn.Open();
    var cmd = conn.CreateCommand();
    cmd.CommandText = "SELECT Nombre FROM Empleados WHERE Id = @id";
    cmd.Parameters.AddWithValue("@id", id);
    
    var resultado = cmd.ExecuteScalar();
    return resultado?.ToString() ?? "";
}

    }
