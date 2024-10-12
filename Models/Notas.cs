namespace SistemaDeListadoMateriasBD.Models
{
	public class Notas
	{
		public string _codigo {  get; set; }
		public int _notas {  get; set; }
		public int _semestre { get; set; }
		public int _UC { get; set; }
		public string _materia { get; set; }

		public Notas(string codigo, string materia, int uC, int notas, int semestre)
		{
			_codigo = codigo;
			_notas = notas;
			_semestre = semestre;
			_UC = uC;
			_materia = materia;
		}
	}
}
