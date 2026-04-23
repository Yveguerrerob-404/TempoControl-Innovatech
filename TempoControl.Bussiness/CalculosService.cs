namespace TempoControl.Business;

public class CalculosService {
    public double ObtenerTotalHoras(DateTime entrada, DateTime salida) {
        // Restamos la salida menos la entrada para obtener el tiempo
        TimeSpan diferencia = salida - entrada;
        
        // Retornamos el total de horas 
        return diferencia.TotalHours;
    }

    // Para el reporte mensual
    // se suman las horas de varios días
    public double SumarHorasDelMes(List<double> listaDeHorasDiarias) {
        double totalMes = 0;
        foreach (var hora in listaDeHorasDiarias) {
            totalMes += hora;
        }
        return totalMes;
    }
}