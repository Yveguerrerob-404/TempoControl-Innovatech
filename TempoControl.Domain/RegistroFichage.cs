namespace TempoControl.Domain;

public class RegistroFichaje {
    public int Id { get; set; } // Identificador del ponchado, ID
    public int EmpleadoId { get; set; } // A que empleado pertenece este ponche
    public DateTime FechaHoraEntrada { get; set; } //Hora de entrada 
    public DateTime? FechaHoraSalida { get; set; } // Hora de salida 
}