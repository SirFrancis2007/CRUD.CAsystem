using CRUD.ASP.CAsystem.Data.Interface;
using CRUD.ASP.CAsystem.Datos;
using CRUD.ASP.CAsystem.Models;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;

namespace CRUD.ASP.CAsystem.Data
{
    public class DataEntityTable : ITabla
    {
        private readonly Conexion _conexion;

        public DataEntityTable(Conexion conexion)
        {
            _conexion = conexion;
        }

        public IEnumerable<Tabla> ObtenerTablas(int idUsuario)
        {
            List<Tabla> tablas = new List<Tabla>();

            using (var conexion = _conexion.conexion())
            {
                string query = "SELECT idTabla, NameTabla FROM BD_CAsystem.Tabla WHERE Usuario_idUsuario = @idUsuario";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tablas.Add(new Tabla
                            {
                                idTabla = reader.GetInt32(0),
                                NameTabla = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return tablas;
        }


        public void CrearTabla(Tabla tabla, int? idUsuario)
        {
            using (var conexion = _conexion.conexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("xNameTabla", tabla.NameTabla);
                parametros.Add("xidusuario", idUsuario);
                parametros.Add("xidProducto", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conexion.Execute("CrearTabla", parametros, commandType: CommandType.StoredProcedure);

                tabla.idTabla = parametros.Get<int>("xidProducto");
            }
        }


        public void EliminarTabla(int idtabla, int? idUsuario)
        {
            using (var conexion = _conexion.conexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("xidTabla", idtabla);
                parametros.Add("xidUsuario", idUsuario);

                conexion.Execute("EliminarTabla", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public int ObtenerIdTabla(string nombreTabla)
        {
            var query = "SELECT idTabla FROM Tabla WHERE NameTabla = @nombreTabla";

            using (var conexion = _conexion.conexion())
            {
                return conexion.QueryFirstOrDefault<int>(query, new { nombreTabla});
            }
        }
    }
}

