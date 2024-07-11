
using System.Text;

namespace Pelusas.Vista;

internal class TextoConsolaBuilder
{
	private const char Intro = '\n';
	private const string Tab1 = "    ";
	private const string Tab2 = "        ";
	private const string Tab3 = "            ";
	private const string OpcionesPregunta = " (s/n): ";

	private readonly StringBuilder _StringBuilder = new();

	private bool _PrimerTexto = true;

	public TextoConsolaBuilder Con1Tab (string contenido)
		=> _AnadirContenido(Tab1, contenido);

	public TextoConsolaBuilder Con1Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(Tab1, contenidos);

	public TextoConsolaBuilder Con2Tab (string contenido)
		=> _AnadirContenido(Tab2, contenido);

	public TextoConsolaBuilder Con2Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(Tab2, contenidos);

	public TextoConsolaBuilder Con3Tab (string contenido)
		=> _AnadirContenido(Tab3, contenido);

	public TextoConsolaBuilder Con3Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(Tab3, contenidos);

	public string Build ()
	{
		return _StringBuilder.ToString();
	}

	public string BuildPregunta ()
	{
		_StringBuilder.Append(OpcionesPregunta);
		return Build();
	}

	private TextoConsolaBuilder _AnadirContenido (
		string tab, string contenido)
	{
		_GestionarPrimerTexto();

		_StringBuilder.Append(Intro);
		_StringBuilder.Append(tab);
		_StringBuilder.Append(contenido);

		return this;
	}

	private TextoConsolaBuilder _AnadirContenidos (
		string tab, IEnumerable<string> contenidos)
	{
		_GestionarPrimerTexto();

		foreach (var contenido in contenidos)
		{
			_StringBuilder.Append(Intro);
			_StringBuilder.Append(tab);
			_StringBuilder.Append(contenido);
		}

		return this;
	}

	private void _GestionarPrimerTexto ()
	{
		if (_PrimerTexto) _PrimerTexto = false;
		else _StringBuilder.Append(Intro);
	}
}
