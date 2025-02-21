using CRUD.ASP.CAsystem.Data.Interface;
using CRUD.ASP.CAsystem.Datos;
using CRUD.ASP.CAsystem.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;

namespace CRUD.ASP.CAsystem.Data
{
    public class DataEntityListAsistencia : IListAsistencia
    {
        private readonly Conexion _conexion;
        public DataEntityListAsistencia(Conexion conexion)
        {
            _conexion = conexion;
        }

        public void CrearAsistencia(Asistente asistente, int? idTabla)
        {
            using (var conexion = _conexion.conexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("xNombre_Asistencia", asistente.Nombre_Asistencia);
                parametros.Add("xDepartamento", asistente.Departamento);
                parametros.Add("xAsistencia", asistente.Asistencia);
                parametros.Add("xTabla_idTabla", idTabla);
                parametros.Add("xidAsistentes", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conexion.Execute("CrearAsistencia", parametros, commandType: CommandType.StoredProcedure);

                asistente.IdAsistente = parametros.Get<int>("xidAsistentes");
            }
        }

        public void EliminarAsistencia(int idAsistente)
        {
            using (var conexion = _conexion.conexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("xidAsistentes", idAsistente);

                conexion.Execute("EliminarAsistencia", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Asistente> ObtenerAsistencias(int idtabla, string nametable)
        {
            List<Asistente> Asistentes = new List<Asistente>();

            using (var conexion = _conexion.conexion())
            {
                string query = @"SELECT idAsistentes, Nombre_Asistencia, Departamento, Asistencia " +
                               "FROM BD_CAsystem.Asistentes " +
                               "JOIN Tabla ON Tabla.idTabla = Asistentes.Tabla_idTabla " +
                               "WHERE Tabla_idTabla = @idtabla AND Tabla.NameTabla = @nametable";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idtabla", idtabla);
                    cmd.Parameters.AddWithValue("@nametable", nametable);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Asistentes.Add(new Asistente
                            {
                                IdAsistente = reader.GetInt32(0),
                                Nombre_Asistencia = reader.GetString(1),
                                Departamento = reader.GetString(2),
                                Asistencia = reader.GetBoolean(3)
                            });
                        }
                    }
                }
            }
            return Asistentes;
        }
    }
}
