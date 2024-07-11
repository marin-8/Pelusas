
namespace Pelusas.Logica;

public sealed class Monton
{
	private readonly List<Carta> _Cartas = [];

	public Monton ()
	{
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Uno, 13));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Dos, 13));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Tres, 13));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Cuatro, 13));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Cinco, 13));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Seis, 9));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Siete, 9));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Ocho, 9));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Nueve, 9));
		_Cartas.AddRange(Carta.Crear(Carta.Valores.Diez, 9));
	}

	public Carta? Coger ()
	{
		if (_Cartas.Count == 0)
			return null;

		var indiceAleatorio = Random.Shared.Next(_Cartas.Count);
		var cartaCogida = _Cartas[indiceAleatorio];
		_Cartas.Remove(cartaCogida);
		return cartaCogida;
	}

	public byte TotalCartas
		=> (byte)_Cartas.Count;
}
