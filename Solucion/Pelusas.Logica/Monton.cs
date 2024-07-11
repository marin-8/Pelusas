
namespace Pelusas.Logica;

public sealed class Monton
{
	private readonly List<Carta> _Cartas;

	public Monton ()
	{
		_Cartas =
			_CrearCartas(
				(ValorCarta.Uno, ValorCarta.Cinco, 13),
				(ValorCarta.Seis, ValorCarta.Diez, 9));
	}

	public Carta? Coger ()
	{
		if (_Cartas.Count == 0)
		{
			return null;
		}

		var indiceAleatorio = Random.Shared.Next(_Cartas.Count);
		var cartaCogida = _Cartas[indiceAleatorio];
		_Cartas.Remove(cartaCogida);

		return cartaCogida;
	}

	public byte TotalCartas
		=> (byte)_Cartas.Count;

	private static List<Carta> _CrearCartas (
		params (ValorCarta Desde, ValorCarta Hasta, byte Cantidad)[] rangos)
	{
		if (rangos.Any(r => r.Desde > r.Hasta))
			throw new ArgumentException(
				"El 'Desde' de un rango no puede ser mayor que el 'Hasta'.",
				nameof(rangos));

		return
			rangos
			.SelectMany(r =>
				Enumerable.Range((int)r.Desde, (int)r.Hasta - (int)r.Desde + 1)
				.SelectMany(v =>
					Enumerable.Range(0, r.Cantidad)
					.Select(_ => new Carta() { Valor = (ValorCarta)v })))
			.ToList();
	}
}
