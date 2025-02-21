using CRUD.ASP.CAsystem.Data.Interface;
using CRUD.ASP.CAsystem.Datos;
using CRUD.ASP.CAsystem.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;
using BCrypt;
using System.Diagnostics.CodeAnalysis;

namespace CRUD.ASP.CAsystem.Data
{
    public class DataEntityUser: IUser
    {
        private readonly Conexion _conexion;

        public DataEntityUser(Conexion conexion)
        {
            _conexion = conexion;
        }

        public void Insertar(User usuario)
        {
            using (var conexion = _conexion.conexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("p_Usuario", usuario.Usuario);
                parametros.Add("p_Password", usuario.contraseña);
                parametros.Add("p_Correo", usuario.correo);
                parametros.Add("p_idusuario", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conexion.Execute("CrearUsuario", parametros, commandType: CommandType.StoredProcedure);

                usuario.Iduser = parametros.Get<int>("p_idusuario");
            }
        }


        public bool Login(string usuario, string contraseña)
        {
            using (var conexion = _conexion.conexion())
            {
                string query = "SELECT Password FROM BD_CAsystem.Usuario WHERE Usuario = @Usuario";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string storedPassword = result.ToString(); 

                        if (storedPassword == contraseña)
                        {
                            return true; 
                        }
                    }
                }
            }
            return false; 
        }

        public int ObtenerIdUsuario(string Usuario)
        {
            var query = "SELECT IdUsuario FROM BD_CAsystem.Usuario WHERE Usuario = @Usuario";

            using (var conexion = _conexion.conexion())
            {
                return conexion.QueryFirstOrDefault<int>(query, new { Usuario = Usuario });
            }
        }
    }
}
