
using Pelusas.Logica.Elementos;

namespace Pelusas.Logica;

public sealed class Resultados
{
	public required string NombreGanador { get; init; }
	public required Jugador[] Jugadores { get; init; }
}
