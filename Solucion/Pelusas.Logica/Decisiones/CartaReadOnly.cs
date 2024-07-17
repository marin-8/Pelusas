
using Pelusas.Logica.Elementos;
using Pelusas.Logica.Enums;

namespace Pelusas.Logica.Decisiones;

public class CartaReadOnly
{
	private readonly Carta _Carta;

	public CartaReadOnly (Carta carta)
	{
		_Carta = carta;
	}

	public ValorCarta Valor => _Carta.Valor;
}
