
using Pelusas.Logica.Decisiones;
using Pelusas.Logica.Elementos;

namespace Pelusas.Logica;

public sealed class Partida
{
	private readonly Jugador[] _Jugadores;
	private readonly Monton _Monton = new();
	private readonly FuncionesDecisiones _Decisiones;

	public Partida (
		FuncionesDecisiones decisiones,
		params string[] nombresJugadores)
	{
		if (nombresJugadores.Length < 2 || nombresJugadores.Length > 6)
			throw new ArgumentException(
				"La cantidad de jugadores tiene que estar entre 2 y 6 (ambos incluídos).",
				nameof(nombresJugadores));

		_Jugadores = nombresJugadores.Select(nj => new Jugador(nj)).ToArray();
		_Decisiones = decisiones;
	}

	public Resultados Jugar ()
	{
		while (true)
		{
			foreach (var jugadorTurno in _Jugadores)
			{
				var restoJugadores = _Jugadores.Except([jugadorTurno]).ToArray();

				_Turno(jugadorTurno, restoJugadores, _Monton, _Decisiones);

				if (_Monton.TotalCartas == 0)
				{
					break;
				}
			}

			if (_Monton.TotalCartas == 0)
			{
				break;
			}
		}

		foreach (var jugador in _Jugadores)
		{
			jugador.RecogerMano();
		}

		return new()
		{
			NombreGanador =
				_Jugadores
				.OrderByDescending(j => j.Puntuacion.SumaPuntos)
				.Select(j => j.Nombre)
				.First(),

			Jugadores = _Jugadores
		};
	}

	private static void _Turno (
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		Monton monton,
		FuncionesDecisiones decisiones)
	{
		_FaseRecoger(jugadorTurno);

		while (true)
		{
			if (monton.TotalCartas == 0)
			{
				return;
			}

			var cartaCogidaMonton =
				_FaseBuscar(jugadorTurno, restoJugadores, monton, decisiones);

			if (cartaCogidaMonton is null)
			{
				return;
			}

			var algunOtroJugadorTieneCartasMismoValor =
				restoJugadores.Any(j =>
					j.Mano.ContieneAlgunaCartaPorValor(cartaCogidaMonton.Valor));

			if (algunOtroJugadorTieneCartasMismoValor)
			{
				_FaseRobar(
					jugadorTurno, restoJugadores, monton, cartaCogidaMonton, decisiones);
			}
		}
	}

	private static bool _PierdeTurno (Jugador jugadorTurno, Carta cartaCogidaMonton)
	{
		return
			jugadorTurno.Mano.ContieneAlgunaCartaPorValor(cartaCogidaMonton.Valor)
			&& jugadorTurno.Mano.TotalCartas >= 3;
	}

	private static void _FaseRecoger (Jugador jugadorTurno)
	{
		jugadorTurno.RecogerMano();
	}

	private static Carta? _FaseBuscar (
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		Monton monton,
		FuncionesDecisiones decisiones)
	{
		var jugadorDecideBuscar =
			decisiones.Buscar(
				new(jugadorTurno, restoJugadores, monton.TotalCartas));

		if (!jugadorDecideBuscar)
		{
			return null;
		}

		var cartaCogidaMonton = monton.Coger();

		if (cartaCogidaMonton is null)
		{
			return null;
		}

		if (_PierdeTurno(jugadorTurno, cartaCogidaMonton))
		{
			jugadorTurno.Mano.Vaciar();
			return null;
		}

		jugadorTurno.Mano.Anadir(cartaCogidaMonton);

		return cartaCogidaMonton;
	}

	private static void _FaseRobar (
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		Monton monton,
		Carta cartaCogidaMonton,
		FuncionesDecisiones decisiones)
	{
		var jugadorDecideRobar =
			decisiones.Robar(
				new(jugadorTurno, restoJugadores, monton.TotalCartas, cartaCogidaMonton));

		if (jugadorDecideRobar)
		{
			var cartasRobadas =
				restoJugadores
				.SelectMany(j => j.Mano.QuitarPorValor(cartaCogidaMonton.Valor))
				.ToList();

			jugadorTurno.Mano.Anadir(cartaCogidaMonton.Valor, cartasRobadas);
		}
	}
}
