using CRUD.ASP.CAsystem.Models;

namespace CRUD.ASP.CAsystem.Data.Interface
{
    public interface IUser
    {
        void Insertar(User usuario);
        bool Login(string Usuario, string contraseña);
        int ObtenerIdUsuario(string Usuario);
    }
}
