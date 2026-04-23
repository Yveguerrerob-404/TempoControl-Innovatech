namespace TempoControl.Domain;

public class Empleado {
    public int Id { get; set; } // ID 
    public string NombreCompleto { get; set; } = string.Empty; // Nombre Completo 
public string Departamento { get; set; } = string.Empty; // Departamento 
public string Posicion { get; set; } = string.Empty; //Posición
        public bool EstaActivo { get; set; } = true; // Para poder desactivar sin borrar 
        public int Activo { get; set; } = 1;
}