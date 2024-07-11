
namespace Pelusas.Logica;

public sealed class Puntuacion
{
	private readonly List<Carta> _Cartas = [];

	public void AnadirCartas (Carta[] cartas)
		=> _Cartas.AddRange(cartas);

	public ushort SumaPuntos
		=> (ushort)_Cartas.Sum(c => (byte)c.Valor);
}
