
using Pelusas.Logica.Elementos;
using Pelusas.Logica.Enums;

namespace Pelusas.Logica.Decisiones;

using IReadOnlyDictionaryCartasPorValor =
	IReadOnlyDictionary<ValorCarta, IReadOnlyCollection<CartaReadOnly>>;

using IReadOnlyCollectionCartas =
	IReadOnlyCollection<CartaReadOnly>;

public class JugadorReadOnly
{
	private readonly Jugador _Jugador;

	public JugadorReadOnly (Jugador jugador)
	{
		_Jugador = jugador;
	}

	public string Nombre => _Jugador.Nombre;

	public IReadOnlyDictionaryCartasPorValor CartasManoDictionary
	{
		get
		{
			return
				_Jugador.Mano.Cartas
				.OrderBy(kvp => kvp.Key)
				.ToDictionary(
					kvp => kvp.Key,
					kvp =>
						kvp.Value.Select(c => new CartaReadOnly(c)).ToArray()
						as IReadOnlyCollection<CartaReadOnly>);
		}
	}


	public IReadOnlyCollectionCartas CartasManoCollection
	{
		get
		{
			return
				_Jugador.Mano.Cartas
				.OrderBy(kvp => kvp.Key)
				.SelectMany(kvp => kvp.Value)
				.Select(c => new CartaReadOnly(c))
				.ToArray();
		}
	}
}
