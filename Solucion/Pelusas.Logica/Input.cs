
namespace Pelusas.Logica;

public sealed class Input // TODO: restringir datos a read only
{
	public required Func<string, Jugador[], byte, bool> Buscar { get; init; }
	public required Func<string, Carta, Jugador[], byte, bool> Robar { get; init; }
}
