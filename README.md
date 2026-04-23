# Sistema TempoControl - Innovatech Solutions, S.R.L.

Este proyecto es desarrollado para la gestión de asistencia de la empresa **Innovatech Solutions, S.R.L.** El sistema automatiza el registro de jornada laboral y genera reportes de nómina precisos.

## Organizacion del Proyecto

El software ha sido diseñado bajo una **Arquitectura en Capas**, garantizando un código mantenible y escalable:

### Capas del Sistema

### TempoControl.UI
Interfaz de usuario basada en consola. Gestiona la interacción directa, los menús y la visualización de reportes.

### TempoControl.Business
Capa de lógica de negocio donde se procesan los cálculos de horas mensuales y las reglas de validación.

### TempoControl.Data
Capa de acceso a datos que utiliza **SQLite** para garantizar que la información de los empleados y sus registros no se pierda al cerrar el programa.

### TempoControl.Domain
Define las entidades núcleo del negocio: Empleado y Registro de Asistencia.

## Funcionalidades del Sistema

### Menu Principal
El sistema ofrece un flujo de trabajo intuitivo con las siguientes opciones:
1. **Registrar Nuevo Empleado:** Alta de personal en la base de datos.
2. **Registrar Entrada:** Captura de hora de inicio de jornada.
3. **Registrar Salida:** Captura de hora de finalización.
4. **Generar Reporte Mensual:** Cálculo detallado de días trabajados y horas totales.
5. **Gestión de Empleados:** Módulo administrativo (Ver lista, Modificar y Dar de baja).
6. **Salir:** Cierre seguro del sistema.

### Reportes Detallados
El módulo de reportes genera un resumen profesional que incluye:
* Nombre del Empleado.
* Total de días laborados en el periodo.
* Cálculo exacto de horas acumuladas en el mes.
