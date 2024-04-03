using Managment.core.Repositories.Facturas.Models;

namespace Managment.core.Repositories.Facturas.Interfaces
{
    internal abstract class FacturasBase : IFacturasRepository
    {
        public abstract IEnumerable<Factura> findFacturasByPersona(int id);
        public abstract void storeFactura(Factura factura);
    }

    public interface IFacturasRepository
    {
        IEnumerable<Factura> findFacturasByPersona(int id);
        void storeFactura(Factura factura);
    }
}
