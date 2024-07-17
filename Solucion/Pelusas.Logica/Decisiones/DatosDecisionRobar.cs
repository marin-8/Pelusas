
using System.Diagnostics.CodeAnalysis;
using Pelusas.Logica.Elementos;

namespace Pelusas.Logica.Decisiones;

public class DatosDecisionRobar : DatosDecisionBuscar
{
	public required CartaReadOnly CartaCogidaMonton { get; init; }

	[SetsRequiredMembers]
	public DatosDecisionRobar (
		Jugador jugadorTurno,
		Jugador[] restoJugadores,
		byte totalCartasMonton,
		Carta cartaCogidaMonton)
		: base(jugadorTurno, restoJugadores, totalCartasMonton)
	{
		CartaCogidaMonton = new(cartaCogidaMonton);
	}
}
