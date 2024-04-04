using Managment.core.Repositories.Facturas.Models;

namespace Managment.core.Repositories.Facturas.Interfaces
{
    internal abstract class FacturasBase : IFacturasRepository
    {
        public abstract Task<IEnumerable<Factura>> findFacturasByPersonaAsync(int id);
        public abstract Task storeFacturaAsync(Factura factura);
    }

    public interface IFacturasRepository
    {
        Task<IEnumerable<Factura>> findFacturasByPersonaAsync(int id);
        Task storeFacturaAsync(Factura factura);
    }
}
