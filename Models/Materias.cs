namespace SistemaDeListadoMateriasBD.Models
{
	public class Materias
	{
		public string? _codigo { get; set; }
		public string? _materia { get; set; }
		public int _semestre { get; set; }
		public int _UC { get; set; }


		public Materias(string? codigo, string? materia, int semestre, int uC)
		{
			_codigo = codigo;
			_materia = materia;
			_semestre = semestre;
			_UC = uC;
		}	
		public Materias() { }
	}
}
