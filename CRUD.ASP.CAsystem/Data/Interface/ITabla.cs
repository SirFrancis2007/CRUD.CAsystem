using CRUD.ASP.CAsystem.Models;

namespace CRUD.ASP.CAsystem.Data.Interface
{
    public interface ITabla
    {
        IEnumerable<Tabla> ObtenerTablas(int idusuario);
        void CrearTabla(Tabla tabla, int? idUsuario);
        void EliminarTabla(int idtabla, int? idUsuario);
        int ObtenerIdTabla(string NombreTabla);
    }
}
