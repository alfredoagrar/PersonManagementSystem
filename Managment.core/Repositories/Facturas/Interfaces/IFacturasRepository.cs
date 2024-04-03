using Managment.core.Repositories.Facturas.Models;

namespace Managment.core.Repositories.Facturas.Interfaces
{
    internal abstract class FacturasBase : IFacturasRepository
    {
        public abstract Factura findFacturaByPersona(int id);
        public abstract void storeFactura(Factura factura);
    }

    public interface IFacturasRepository
    {
        Factura findFacturaByPersona(int id);
        void storeFactura(Factura factura);
    }
}
