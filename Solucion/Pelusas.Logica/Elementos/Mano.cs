
using Pelusas.Logica.Enums;

namespace Pelusas.Logica.Elementos;

public sealed class Mano
{
	private readonly Dictionary<ValorCarta, List<Carta>> _Cartas = [];

	public IReadOnlyDictionary<ValorCarta, List<Carta>> Cartas => _Cartas;

	public void Anadir (Carta carta)
	{
		if (_Cartas.TryGetValue(carta.Valor, out var cartasActuales))
		{
			cartasActuales.Add(carta);
		}
		else
		{
			_Cartas.Add(carta.Valor, [carta]);
		}
	}

	public void Anadir (ValorCarta valor, List<Carta> cartas)
	{
		if (_Cartas.TryGetValue(valor, out var cartasActuales))
		{
			cartasActuales.AddRange(cartas);
		}
		else
		{
			_Cartas.Add(valor, cartas);
		}
	}

	public bool ContieneAlgunaCartaPorValor (ValorCarta valor)
		=> _Cartas.ContainsKey(valor);

	public byte TotalCartas
		=> (byte)_Cartas.Values.Sum(v => v.Count);

	public Carta[] Vaciar ()
	{
		if (_Cartas.Count == 0)
		{
			return [];
		}

		var cartas = _Cartas.Values.SelectMany(lc => lc).ToArray();
		_Cartas.Clear();

		return cartas;
	}

	public List<Carta> QuitarPorValor (ValorCarta valor)
	{
		if (_Cartas.TryGetValue(valor, out var cartasRobadas))
		{
			_Cartas.Remove(valor);
			return cartasRobadas;
		}
		else
		{
			return [];
		}
	}
}
