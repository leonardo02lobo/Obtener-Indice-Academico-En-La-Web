using MySql.Data.MySqlClient;

namespace SistemaDeListadoMateriasBD.Controller
{
	public class Conexion
	{
		private string _servidor = "localhost";
		private string _bd = "mysql";
		private string _usuario = "root";
		private string _contrasenia = "root1234";
		public MySqlConnection miConexion;

		public Conexion()
		{
			string cadenaConexion = $"Database={_bd}; Data Source={ _servidor }; User Id={_usuario }; Password={_contrasenia}";
			miConexion = new MySqlConnection(cadenaConexion);

		}
	}
}
