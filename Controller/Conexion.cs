using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SistemaDeListadoMateriasBD.Models;

namespace SistemaDeListadoMateriasBD.Controller
{
    public class Conexion
    {
        //private string _servidor = "localhost";
        //private string _bd = "mysql";
        //private string _usuario = "root";
        //private string _contrasenia = "root1234";
        private string _servidor = "bkxv1cfk2iueraku6byw-mysql.services.clever-cloud.com";
        private string _puerto = "3306";
        private string _usuario = "u5e4nmaddbkt8jwj";
        private string _contrasenia = "qux15guPHiJQs4AUwQOc";
        public static string nombreBD = "bkxv1cfk2iueraku6byw";
        public MySqlConnection miConexion;

        public Conexion()
        {
            string cadenaConexion = $"Database={nombreBD}; Data Source={_servidor}; Port={_puerto}; User Id={_usuario}; Password={_contrasenia}";
            miConexion = new MySqlConnection(cadenaConexion);
        }

        public void InsertarDatos(Persona persona)
        {
            miConexion.Open();
            string consulta = $"insert into {nombreBD}.user(nombre,apellido,cedula,carrera,contrasenia,usuario) value ('{persona.nombre}','{persona.apellido}','{persona.cedula}','{persona.carrera}','{persona.contrasenia}','{persona.usuario}');";
            MySqlCommand comando = new MySqlCommand(consulta, miConexion);
            comando.ExecuteNonQuery();
        }

        public void CrearTabla(Persona persona)
        {
            List<string> codigos = new List<string>();
            List<int> semestre = new List<int>();
            List<int> UC = new List<int>();

            string consulta = $"create table {nombreBD}.notas_{persona.nombre}_{persona.cedula} (" +
                "cedula int," +
                "codigo varchar(50), " +
                "notas int, " +
                "semestre int," +
                " UC int," +
                "carrera varchar(100)" +
                ");";
            MySqlCommand comando = new MySqlCommand(consulta, miConexion);
            comando.ExecuteNonQuery();

            string[] carrera = persona.carrera.ToLower().Split(" ");
            string carreraAsignada = carrera[1] switch
            {
                "informatica" => "Ingenieria Informatica",
                "electronica" => "Ingenieria Electronica"
            };

            int cantidadDatos = 0;
            comando = new MySqlCommand($"select codigo,semestre,UC from {nombreBD}.materias_{carrera[1]};", miConexion);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                cantidadDatos++;
                codigos.Add(reader.GetString(0));
                semestre.Add(reader.GetInt32(1));
                UC.Add(reader.GetInt32(2));
            }
            reader.Close();

            for (int i = 0; i < cantidadDatos; i++)
            {
                consulta = $"insert into {nombreBD}.notas_{persona.nombre}_{persona.cedula} (cedula,codigo,notas,semestre,UC,carrera) values ({persona.cedula},'{codigos[i]}',0,{semestre[i]},{UC[i]},'{carreraAsignada}');";
                comando = new MySqlCommand(consulta, miConexion);
                comando.ExecuteNonQuery();
            }
        }


        public async Task ActualizarDatos(string carrera, string materia, int nota, string nombre, string cedula)
        {
            string codigo = "";
            miConexion.Open();
            MySqlCommand command2 = new MySqlCommand($"select codigo from {nombreBD}.materias_{carrera} where materia = '{materia}';", miConexion);
            MySqlDataReader reader = command2.ExecuteReader();

            while (await reader.ReadAsync())
            {
                codigo = reader.GetString(0);
            }
            reader.Close();
            string guardar = $"UPDATE {nombreBD}.notas_{nombre}_{cedula} SET {Conexion.nombreBD}.notas_{nombre}_{cedula}.notas = {nota} WHERE codigo = '{codigo}' limit 1;";
            MySqlCommand command = new MySqlCommand(guardar, miConexion);
            await command.ExecuteNonQueryAsync();
        }

        public void CerrarConexion() => miConexion.Close();
    }
}
