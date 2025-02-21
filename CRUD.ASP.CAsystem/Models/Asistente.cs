namespace CRUD.ASP.CAsystem.Models
{
    public class Asistente
    {
        public int IdAsistente { get; set; }
        public string Nombre_Asistencia { get; set; }
        public string Departamento { get; set; }
        public bool Asistencia { get; set; }
        Tabla Tabla { get; set; }
    }
}
