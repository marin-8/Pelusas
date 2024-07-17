
using Pelusas.Estrategias.Variables;

namespace Pelusas.Estrategias;

internal readonly struct Estrategia : IEquatable<Estrategia>
{
	public required RiesgosPerderTurno MaximoRiesgoPerderTurno { get; init; }
	public required PuntosARobar MinimosPuntosARobar { get; init; }

	public override bool Equals (object? otroObjeto)
	{
		return
			otroObjeto is not null
			&& otroObjeto is Estrategia otraEstrategia
			&& Equals(otraEstrategia);
	}

	public bool Equals (Estrategia otraEstrategia)
	{
		return
			MaximoRiesgoPerderTurno == otraEstrategia.MaximoRiesgoPerderTurno
			&& MinimosPuntosARobar == otraEstrategia.MinimosPuntosARobar;
	}

	public override int GetHashCode ()
		=> HashCode.Combine(MaximoRiesgoPerderTurno, MinimosPuntosARobar);

	public static bool operator == (Estrategia estrategiaA, Estrategia estrategiaB)
		=> estrategiaA.Equals(estrategiaB);

	public static bool operator != (Estrategia estrategiaA, Estrategia estrategiaB)
		=> !(estrategiaA == estrategiaB);
}
