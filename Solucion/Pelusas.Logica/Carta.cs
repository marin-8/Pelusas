
namespace Pelusas.Logica;

public sealed class Carta : IComparable<Carta>
{
	public required Valores Valor { get; init; }

	public static Carta Crear (Valores valor) =>
		new() { Valor = valor };

	public static Carta[] Crear (Valores valor, byte cantidad)
		=> Enumerable.Range(0, cantidad).Select(_ => Crear(valor)).ToArray();

	public enum Valores : byte
	{
		Uno = 1,
		Dos = 2,
		Tres = 3,
		Cuatro = 4,
		Cinco = 5,
		Seis = 6,
		Siete = 7,
		Ocho = 8,
		Nueve = 9,
		Diez = 10,
	}

	private Carta () { }

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
