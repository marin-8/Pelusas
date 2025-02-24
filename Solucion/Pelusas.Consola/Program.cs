﻿
using System.Reflection;
using Pelusas.Helpers;
using Pelusas.Logica;
using Pelusas.Logica.Decisiones;
using Pelusas.Logica.Elementos;
using Pelusas.Vista;

namespace Pelusas.Consola;

internal sealed class Program
{
	private const byte _CantidadCaracteresNombreJugadorMasLargo = 5;

	private static readonly FuncionesDecisiones _Decisiones =
		new() { Buscar = _Buscar, Robar = _Robar };

	private static void Main ()
	{
		while (true)
		{
			var nombresJugadores = _PedirNombresJugadores();
			var partida = new Partida(_Decisiones, nombresJugadores);
			var resultados = partida.Jugar();
			var volverAJugar = _PreguntarSiVolverAJugar(resultados);

			if (!volverAJugar)
			{
				return;
			}
		}
	}

	private static string[] _PedirNombresJugadores ()
	{
		return ConsolaManager.PedirTextos(
			o => { o.MinimaCantidad = 2; o.MaximaCantidad = 6; },
			(tcb, js) =>
				tcb
				.Con1Tab("PELUSAS (de Reiner Knizia)")
					.Con2Tab(_ObtenerCreditosCodigo())
				.Con1Tab("NUEVA PARTIDA")
					.Con2Tab("Introduce entre 2 y 6 jugadores.")
					.Con2Tab($"Jugadores introducidos: {js.JoinStrings(", ")}")
				.Con1Tab(
					["Escribe el nombre de un/a nuevo/a jugador/a",
					"o no escribas nada para terminar",
					"y pulsa intro: "]));
	}

	private static string _ObtenerCreditosCodigo ()
	{
		var assembly = Assembly.GetExecutingAssembly();

		var autor =
			assembly.GetCustomAttributes<AssemblyCompanyAttribute>()
			.Select(aca => aca.Company)
			.First();

		var version = assembly.GetName().Version!.ToString(2);

		return $"Código de [{autor}]. Versión [{version}].";
	}

	private static bool _Buscar (
		DatosDecisionBuscar datosDecisionBuscar)
	{
		var nombreJugadorTurno = datosDecisionBuscar.JugadorTurno.Nombre;
		var jugadores = datosDecisionBuscar.Jugadores;
		var totalCartasMonton = datosDecisionBuscar.TotalCartasMonton;

		return ConsolaManager.PreguntarSiNo(tb => tb
			.Con1Tab($"FASE DE BUSCAR ({nombreJugadorTurno})")
				.Con2Tab("Manos jugadores:")
					.Con3Tab(_FormatearCartasManos(jugadores))
				.Con2Tab($"Total cartas en el montón: {totalCartasMonton}")
			.Con1Tab("¿Quieres coger una carta del montón?"));
	}

	private static bool _Robar (
		DatosDecisionRobar datosDecisionRobar)
	{
		var nombreJugadorTurno = datosDecisionRobar.JugadorTurno.Nombre;
		var cartaCogidaMonton = datosDecisionRobar.CartaCogidaMonton;
		var jugadores = datosDecisionRobar.Jugadores;
		var totalCartasMonton = datosDecisionRobar.TotalCartasMonton;

		return ConsolaManager.PreguntarSiNo(tb => tb
			.Con1Tab($"FASE DE ROBAR ({nombreJugadorTurno})")
				.Con2Tab("Manos jugadores:")
					.Con3Tab(_FormatearCartasManos(jugadores))
				.Con2Tab($" Carta cogida del montón: {((byte)cartaCogidaMonton.Valor)}")
				.Con2Tab($"Total cartas en el montón: {totalCartasMonton}")
			.Con1Tab("¿Quieres robar a tus oponentes sus cartas con el mismo valor?"));
	}

	private static bool _PreguntarSiVolverAJugar (Resultados resultados)
	{
		return ConsolaManager.PreguntarSiNo(tb => tb
			.Con1Tab("FIN DE LA PARTIDA")
				.Con2Tab("Puntuaciones:")
					.Con3Tab(_FormatearPuntuaciones(resultados.Jugadores))
				.Con2Tab($"Ganador/a: {resultados.NombreGanador}")
			.Con1Tab("¿Quieres volver a jugar?"));
	}

	private static IEnumerable<string> _FormatearCartasManos (
		JugadorReadOnly[] jugadores)
	{
		return jugadores.Select(j =>
			$"{j.Nombre.PadRight(_CantidadCaracteresNombreJugadorMasLargo)}: " +
			$"{_FormatearCartasMano(j)}");
	}

	private static string _FormatearCartasMano (JugadorReadOnly jugador)
	{
		return
			jugador.CartasManoCollection
			.Select(c => ((byte)c.Valor).ToString())
			.JoinStrings(" - ");
	}

	private static IEnumerable<string> _FormatearPuntuaciones (Jugador[] jugadores)
	{
		return
			jugadores
			.Select(j =>
				$"{j.Nombre.PadRight(_CantidadCaracteresNombreJugadorMasLargo)}: " +
				$"{j.Puntuacion.SumaPuntos}");
	}
}
