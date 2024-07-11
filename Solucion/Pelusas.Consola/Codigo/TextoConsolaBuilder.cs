
using System.Text;

namespace Pelusas.Vista;

internal sealed class TextoConsolaBuilder
{
	private const char _Intro = '\n';
	private const string _Tab1 = "    ";
	private const string _Tab2 = "        ";
	private const string _Tab3 = "            ";
	private const string _OpcionesPregunta = " (s/n): ";

	private readonly StringBuilder _StringBuilder = new();

	private bool _PrimerTexto = true;

	public TextoConsolaBuilder Con1Tab (string contenido)
		=> _AnadirContenido(_Tab1, contenido);

	public TextoConsolaBuilder Con1Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(_Tab1, contenidos);

	public TextoConsolaBuilder Con2Tab (string contenido)
		=> _AnadirContenido(_Tab2, contenido);

	public TextoConsolaBuilder Con2Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(_Tab2, contenidos);

	public TextoConsolaBuilder Con3Tab (string contenido)
		=> _AnadirContenido(_Tab3, contenido);

	public TextoConsolaBuilder Con3Tab (IEnumerable<string> contenidos)
		=> _AnadirContenidos(_Tab3, contenidos);

	public string Build ()
		=> _StringBuilder.ToString();

	public string BuildPregunta ()
	{
		_StringBuilder.Append(_OpcionesPregunta);
		return Build();
	}

	private TextoConsolaBuilder _AnadirContenido (
		string tab, string contenido)
	{
		_GestionarPrimerTexto();

		_StringBuilder.Append(_Intro);
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
			_StringBuilder.Append(_Intro);
			_StringBuilder.Append(tab);
			_StringBuilder.Append(contenido);
		}

		return this;
	}

	private void _GestionarPrimerTexto ()
	{
		if (_PrimerTexto) _PrimerTexto = false;
		else _StringBuilder.Append(_Intro);
	}
}
