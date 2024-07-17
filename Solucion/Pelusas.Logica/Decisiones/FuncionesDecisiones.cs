
namespace Pelusas.Logica.Decisiones;

public sealed class FuncionesDecisiones
{
    public required Func<DatosDecisionBuscar, bool> Buscar { get; init; }
    public required Func<DatosDecisionRobar, bool> Robar { get; init; }
}
