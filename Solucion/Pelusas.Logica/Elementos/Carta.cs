
using Pelusas.Logica.Enums;

namespace Pelusas.Logica.Elementos;

public sealed class Carta : IComparable<Carta>
{
	public required ValorCarta Valor { get; init; }

	public int CompareTo (Carta? otraCarta)
		=> otraCarta != null ? Valor.CompareTo(otraCarta.Valor) : 1;

	public override bool Equals (object? otroObjeto)
	{
		return
			otroObjeto is not null
			&& otroObjeto is Carta otraCarta
			&& otraCarta.Valor == Valor;
	}

	public override int GetHashCode ()
		=> Valor.GetHashCode();
}
