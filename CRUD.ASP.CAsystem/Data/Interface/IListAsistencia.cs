using CRUD.ASP.CAsystem.Models;

namespace CRUD.ASP.CAsystem.Data.Interface
{
    public interface IListAsistencia
    {
        IEnumerable<Asistente> ObtenerAsistencias(int idtabla, string nametable);
        void CrearAsistencia(Asistente asistente, int? idTabla);
        void EliminarAsistencia(int idAsistente);
    }
}
