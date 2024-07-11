
namespace Pelusas.Logica;

public sealed class Partida
{
	private readonly Jugador[] _Jugadores;
	private readonly Monton _Monton = new();
	private readonly Input _InputJugador;

	public Partida (
		Input input,
		params string[] nombresJugadores)
	{
		if (nombresJugadores.Length < 2 || nombresJugadores.Length > 6)
			throw new ArgumentOutOfRangeException(
				nameof(nombresJugadores),
				"La cantidad de jugadores tiene que estar entre 2 y 6 (ambos incluídos).");

		_Jugadores = nombresJugadores.Select(nj => new Jugador(nj)).ToArray();
		_InputJugador = input;
	}

	public Resultados Jugar ()
	{
		while (true)
		{
			foreach (var jugador in _Jugadores)
			{
				var restoJugadores = _Jugadores.Except([jugador]).ToArray();

				_Turno(_Jugadores, jugador, restoJugadores, _Monton, _InputJugador);

				if (_Monton.TotalCartas == 0) break;
			}

			if (_Monton.TotalCartas == 0) break;
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
		Jugador[] todosJugadores,
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		Monton monton,
		Input input)
	{
		_FaseRecoger(jugadorTurno);

		while (true)
		{
			if (monton.TotalCartas == 0) return;

			var cartaCogidaMonton =
				_FaseBuscar(todosJugadores, jugadorTurno, monton, input);

			if (cartaCogidaMonton is null) return;

			var algunOtroJugadorTieneCartasMismoValor =
				restoJugadores.Any(j =>
					j.Mano.ContieneAlgunaCartaPorValor(cartaCogidaMonton.Valor));

			if (algunOtroJugadorTieneCartasMismoValor)
			{
				_FaseRobar(
					todosJugadores,
					jugadorTurno,
					restoJugadores,
					monton,
					cartaCogidaMonton,
					input);
			}
		}
	}

	private static bool _PierdeTurno (Jugador jugador, Carta cartaCogidaMonton)
	{
		return
			jugador.Mano.ContieneAlgunaCartaPorValor(cartaCogidaMonton.Valor)
			&& jugador.Mano.TotalCartas >= 3;
	}

	private static void _FaseRecoger (Jugador jugador)
	{
		jugador.RecogerMano();
	}

	private static Carta? _FaseBuscar (
		Jugador[] todosJugadores,
		Jugador jugadorTurno,
		Monton monton,
		Input input)
	{
		if (!input.Buscar(jugadorTurno.Nombre, todosJugadores, monton.TotalCartas))
			return null;

		var cartaCogidaMonton = monton.Coger();
		if (cartaCogidaMonton is null) return null;

		if (_PierdeTurno(jugadorTurno, cartaCogidaMonton))
		{
			jugadorTurno.Mano.Vaciar();
			return null;
		}

		jugadorTurno.Mano.Anadir(cartaCogidaMonton);

		return cartaCogidaMonton;
	}

	private static void _FaseRobar (
		Jugador[] todosJugadores,
		Jugador jugador,
		Jugador[] restoJugadores,
		Monton monton,
		Carta cartaCogidaMonton,
		Input input)
	{
		var jugadorDecideRobar =
			input.Robar(
				jugador.Nombre, cartaCogidaMonton, todosJugadores, monton.TotalCartas);

		if (jugadorDecideRobar)
		{
			var cartasRobadas =
				restoJugadores
				.SelectMany(j => j.Mano.QuitarPorValor(cartaCogidaMonton.Valor))
				.ToList();

			jugador.Mano.Anadir(cartaCogidaMonton.Valor, cartasRobadas);
		}
	}
}
