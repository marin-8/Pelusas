
using System.Diagnostics;
using Pelusas.Estrategias.Variables;
using Pelusas.Logica;
using Pelusas.Logica.Decisiones;
using Pelusas.Logica.Elementos;

namespace Pelusas.Estrategias;

internal sealed class Program
{
	private static readonly string[] _NombresJugadores = ["Jugador1", "Jugador2"];

	private static readonly	Dictionary<(Estrategia EstrategiaJugador1, Estrategia EstrategiaJugador2), ResultadosAcumulados> Resultados = [];

	private class ResultadosAcumulados
	{
		public required byte VecesGanorJugador1 { get; set; }
		public required byte VecesGanorJugador2 { get; set; }
	}

	private static void Main ()
	{
		var indicePartida = 1;

		var stopwatch = new Stopwatch();
		stopwatch.Start();

		foreach (var estrategiaJugador1 in _GenerarEstrategias())
		{
			foreach (var estrategiaJugador2 in _GenerarEstrategias())
			{
				if (estrategiaJugador1 == estrategiaJugador2)
				{
					continue;
				}

				var decisiones = new FuncionesDecisiones()
				{
					Buscar =
						ddb => _Buscar(
							ddb, new() {
								{ _NombresJugadores[0], estrategiaJugador1 },
								{ _NombresJugadores[1], estrategiaJugador2 } }),

					Robar =
						ddr => _Robar(
							ddr, new() {
								{ _NombresJugadores[0], estrategiaJugador1 },
								{ _NombresJugadores[1], estrategiaJugador2 } }),
				};

				for (int i = 0; i < 100; i++)
				{
					var partida = new Partida(decisiones, _NombresJugadores);
					var resultados = partida.Jugar();

					if (Resultados.TryGetValue(
						(estrategiaJugador1, estrategiaJugador2),
						out var resultadosAcumuladosPorEstrategias))
					{
						if (resultados.NombreGanador == _NombresJugadores[0])
						{
							resultadosAcumuladosPorEstrategias.VecesGanorJugador1++;
						}
						else
						{
							resultadosAcumuladosPorEstrategias.VecesGanorJugador2++;
						}
					}
					else
					{
						if (resultados.NombreGanador == _NombresJugadores[0])
						{
							Resultados.Add(
								(estrategiaJugador1, estrategiaJugador2),
								new() { VecesGanorJugador1 = 1, VecesGanorJugador2 = 0 });
						}
						else
						{
							Resultados.Add(
								(estrategiaJugador1, estrategiaJugador2),
								new() { VecesGanorJugador1 = 0, VecesGanorJugador2 = 1 });
						}
					}

					if (indicePartida % 10000 == 0 || indicePartida == 10_857_000)
					{
						var indicePartidaFormateado =
							indicePartida.ToString("N0").PadLeft(10, ' ');

						Console.WriteLine(
							$"Terminada partida {indicePartidaFormateado} / 10.085.700");
					}

					indicePartida++;
				}
			}
		}

		stopwatch.Stop();

		var tiempoTranscurrido = stopwatch.Elapsed;

		var tiempoTranscurridoFormateado =
			string.Format(
				"{0:00}:{1:00}.{2:000}",
				tiempoTranscurrido.Minutes,
				tiempoTranscurrido.Seconds,
				tiempoTranscurrido.Milliseconds);

		Console.WriteLine(
			$"\nTerminadas todas las partidas en {tiempoTranscurridoFormateado} minutos");

		Console.ReadKey();
	}

	private static IEnumerable<Estrategia> _GenerarEstrategias ()
	{
		foreach (var riesgoPerderTurno in Enum.GetValues<RiesgosPerderTurno>())
		{
			foreach (var puntosARobar in Enum.GetValues<PuntosARobar>())
			{
				yield return new()
				{
					MaximoRiesgoPerderTurno = riesgoPerderTurno,
					MinimosPuntosARobar = puntosARobar
				};
			}
		}
	}

	private static bool _Buscar (
		DatosDecisionBuscar datosDecisionBuscar,
		Dictionary<string, Estrategia> estrategiasPorJugador)
	{
		var nombreJugadorTurno = datosDecisionBuscar.JugadorTurno.Nombre;

		var maximoRiesgoPerderTurno =
			estrategiasPorJugador[nombreJugadorTurno].MaximoRiesgoPerderTurno;

		var riesgoPerderTurno =
			datosDecisionBuscar.JugadorTurno.CartasManoCollection.Count < 3
				? 0
				: datosDecisionBuscar.JugadorTurno.CartasManoDictionary.Keys.Count();

		return riesgoPerderTurno <= (int)maximoRiesgoPerderTurno;
	}

	private static bool _Robar (
		DatosDecisionRobar datosDecisionRobar,
		Dictionary<string, Estrategia> estrategiasPorJugador)
	{
		var nombreJugadorTurno = datosDecisionRobar.JugadorTurno.Nombre;
		var valorCartaCogidaMonton = datosDecisionRobar.CartaCogidaMonton.Valor;

		var minimosPuntosARobar =
			estrategiasPorJugador[nombreJugadorTurno].MinimosPuntosARobar;

		var puntosRobables =
			datosDecisionRobar.RestoJugadores.Sum(j =>
				j.CartasManoDictionary[valorCartaCogidaMonton].Sum(c => (byte)c.Valor));

		return puntosRobables >= (int)minimosPuntosARobar;
	}
}
