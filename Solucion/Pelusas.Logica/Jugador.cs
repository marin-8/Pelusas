
namespace Pelusas.Logica;

public sealed class Jugador
{
	public string Nombre { get; init; }
	public Mano Mano { get; } = new();
	public Puntuacion Puntuacion { get; } = new();

	public Jugador (string nombre) { Nombre = nombre; }

	public void RecogerMano ()
	{
		var cartas = Mano.Vaciar();
		Puntuacion.AnadirCartas(cartas);
	}
}
