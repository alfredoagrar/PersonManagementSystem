using Managment.core.Repositories.Facturas.Models;

namespace Managment.core.Repositories.Facturas.Interfaces
{
    /// <summary>
    /// Clase base abstracta para el repositorio de facturas.
    /// </summary>
    internal abstract class FacturasBase : IFacturasRepository
    {
        /// <summary>
        /// Busca las facturas por persona de forma asincrónica.
        /// </summary>
        /// <param name="id">El ID de la persona.</param>
        /// <returns>Una colección de facturas.</returns>
        public abstract Task<IEnumerable<Factura>> findFacturasByPersonaAsync(int id);

        /// <summary>
        /// Almacena una factura de forma asincrónica.
        /// </summary>
        /// <param name="factura">La factura a almacenar.</param>
        public abstract Task storeFacturaAsync(Factura factura);
    }

    /// <summary>
    /// Interfaz para el repositorio de facturas.
    /// </summary>
    public interface IFacturasRepository
    {
        /// <summary>
        /// Busca las facturas por persona de forma asincrónica.
        /// </summary>
        /// <param name="id">El ID de la persona.</param>
        /// <returns>Una colección de facturas.</returns>
        Task<IEnumerable<Factura>> findFacturasByPersonaAsync(int id);

        /// <summary>
        /// Almacena una factura de forma asincrónica.
        /// </summary>
        /// <param name="factura">La factura a almacenar.</param>
        Task storeFacturaAsync(Factura factura);
    }
}
