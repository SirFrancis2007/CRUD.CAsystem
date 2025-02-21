using MySql.Data.MySqlClient;

namespace CRUD.ASP.CAsystem.Datos
{
    public class Conexion
    {
        private readonly string _connectionstring;
        public Conexion(string valor)
        {
            _connectionstring = valor;
        }

        public MySqlConnection conexion()
        {
            try
            {
                var conexion = new MySqlConnection(_connectionstring);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
