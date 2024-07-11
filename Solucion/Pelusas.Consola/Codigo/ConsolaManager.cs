
namespace Pelusas.Vista;

internal static class ConsolaManager
{
	public static bool PreguntarSiNo (
		Action<TextoConsolaBuilder> textoConsolaBuilderAction)
	{
		var textoConsolaBuilder = new TextoConsolaBuilder();
		textoConsolaBuilderAction(textoConsolaBuilder);
		var textoConsola = textoConsolaBuilder.BuildPregunta();

		while (true)
		{
			Console.Clear();
			Console.Write(textoConsola);

			var consoleKeyInfo = Console.ReadKey();

			if (consoleKeyInfo.Key != ConsoleKey.S
				&& consoleKeyInfo.Key != ConsoleKey.N)
			{
				continue;
			}

			return consoleKeyInfo.Key == ConsoleKey.S;
		}
	}

	public static string[] PedirTextos (
		Action<OpcionesPedirTextos> modificarOpciones,
		Action<TextoConsolaBuilder, IReadOnlyCollection<string>> textoConsolaBuilderAction)
	{
		var opcionesPedirTextos = new OpcionesPedirTextos();
		modificarOpciones(opcionesPedirTextos);

		var textosIntroducido = new List<string>();
		string? textoIntroducido;

		while (textosIntroducido.Count < opcionesPedirTextos.MaximaCantidad)
		{
			var textoConsolaBuilder = new TextoConsolaBuilder();
			textoConsolaBuilderAction(textoConsolaBuilder, textosIntroducido);
			var textoConsola = textoConsolaBuilder.Build();

			Console.Clear();
			Console.Write(textoConsola);

			textoIntroducido = Console.ReadLine();

			if (textoIntroducido is null) continue;

			if (textoIntroducido == opcionesPedirTextos.Terminador)
			{
				if (textosIntroducido.Count >= opcionesPedirTextos.MinimaCantidad)
				{
					break;
				}
			}
			else if (!textosIntroducido.Contains(textoIntroducido))
			{
				textosIntroducido.Add(textoIntroducido);
			}
		}

		return textosIntroducido.ToArray();
	}

	public class OpcionesPedirTextos
	{
		public byte MinimaCantidad { get; set; } = 0;
		public byte MaximaCantidad { get; set; } = 255;
		public string Terminador { get; set; } = "";
	}
}
