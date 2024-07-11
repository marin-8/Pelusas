
namespace Pelusas.Logica;

public sealed class Decisiones // TODO: restringir datos a readonly
{
	public required Func<string, Jugador[], byte, bool> Buscar { get; init; }
	public required Func<string, Carta, Jugador[], byte, bool> Robar { get; init; }
}
