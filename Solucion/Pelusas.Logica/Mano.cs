
namespace Pelusas.Logica;

public sealed class Mano
{
	public Dictionary<Carta.Valores, List<Carta>> Cartas { get; init; } = [];

	public void Anadir (Carta carta)
	{
		if (Cartas.TryGetValue(carta.Valor, out var cartas))
			cartas.Add(carta);
		else
		{
			Cartas.Add(carta.Valor, [carta]);
		}
	}

	public void Anadir (Carta.Valores valor, List<Carta> cartas)
	{
		if (Cartas.TryGetValue(valor, out var cartasActuales))
			cartasActuales.AddRange(cartas);
		else
		{
			Cartas.Add(valor, cartas);
		}
	}

	public bool ContieneAlgunaCartaPorValor (Carta.Valores valor)
		=> Cartas.ContainsKey(valor);

	public byte TotalCartas
		=> (byte)Cartas.Values.Sum(v => v.Count);

	public Carta[] Vaciar ()
	{
		if (Cartas.Count == 0) return [];
		var cartas = Cartas.Values.SelectMany(lc => lc).ToArray();
		Cartas.Clear();
		return cartas;
	}

	public List<Carta> QuitarPorValor (Carta.Valores valor)
	{
		if (Cartas.TryGetValue(valor, out var cartasRobadas))
		{
			Cartas.Remove(valor);
			return cartasRobadas;
		}
		else
		{
			return [];
		}
	}
}
