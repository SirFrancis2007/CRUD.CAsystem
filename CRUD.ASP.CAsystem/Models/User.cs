namespace CRUD.ASP.CAsystem.Models
{
    public class User
    {
        public int? Iduser { get; set; }
        public required string Usuario { get; set; }
        public required string contraseña { get; set; }
        public required string correo { get; set; }
    }
}
