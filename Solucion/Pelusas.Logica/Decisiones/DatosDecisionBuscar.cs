
using System.Diagnostics.CodeAnalysis;
using Pelusas.Logica.Elementos;

namespace Pelusas.Logica.Decisiones;

using IReadOnlyCollectionJugadores = IReadOnlyCollection<JugadorReadOnly>;

public class DatosDecisionBuscar
{
	public required JugadorReadOnly JugadorTurno { get; init; }
	public required IReadOnlyCollectionJugadores RestoJugadores { get; init; }
	public required byte TotalCartasMonton { get; init; }

	public JugadorReadOnly[] Jugadores
		=> RestoJugadores.Concat([JugadorTurno]).ToArray();

	[SetsRequiredMembers]
	public DatosDecisionBuscar (
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		byte totalCartasMonton)
	{
		JugadorTurno = new(jugadorTurno);
		RestoJugadores = restoJugadores.Select(j => new JugadorReadOnly(j)).ToArray();
		TotalCartasMonton = totalCartasMonton;
	}
}
