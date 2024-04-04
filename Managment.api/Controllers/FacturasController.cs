using Managment.api.Models;
using Managment.core.Repositories.Facturas.Interfaces;
using Managment.core.Repositories.Facturas.Models;
using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Managment.api.Controllers
{
    /// <summary>
    /// Controlador para las operaciones relacionadas con las facturas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly IPersonasRepository _personasRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FacturasController"/>.
        /// </summary>
        /// <param name="facturasRepository">Repositorio de facturas.</param>
        /// <param name="personasRepository">Repositorio de personas.</param>
        public FacturasController(IFacturasRepository facturasRepository, IPersonasRepository personasRepository)
        {
            _facturasRepository = facturasRepository;
            _personasRepository = personasRepository;
        }

        /// <summary>
        /// Obtiene las facturas de una persona por su ID.
        /// </summary>
        /// <param name="PersonaId">ID de la persona.</param>
        /// <returns>Una lista de facturas de la persona.</returns>
        [HttpGet("{PersonaId}")]
        public async Task<ActionResult<IEnumerable<Factura>>> FindByPersonaIdAsync(int PersonaId)
        {
            IEnumerable<Factura> facturas = await this._facturasRepository.findFacturasByPersonaAsync(PersonaId);
            ApiResponse<IEnumerable<Factura>> result = new ApiResponse<IEnumerable<Factura>>();
            result.Success = true;
            result.Message = facturas.Any() ? "Información ejecutada correctamente" : "No hay información para recuperar.";
            result.Data = facturas;
            return Ok(result);
        }

        /// <summary>
        /// Almacena una nueva factura para una persona.
        /// </summary>
        /// <param name="newFactura">Datos de la nueva factura.</param>
        /// <returns>La respuesta de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult<Persona>> StorePersonaAsync([FromBody] FacturaDto newFactura)
        {
            Persona? persona = await _personasRepository.findPersonaById(newFactura.PersonaId);
            ApiResponse<string?> result = new ApiResponse<string?>();

            if(persona is null)
            {
                result.Success = true;
                result.Message = $"No se puede agregar la factura porque no existe una persona con el ID {newFactura.PersonaId}.";
                result.Data = null;
                return Ok(result);
            }

            Factura factura = new Factura()
            {
                PersonaId = newFactura.PersonaId,
                Monto = newFactura.Monto,
                FechaEmision = newFactura.FechaEmision,
            };

            await this._facturasRepository.storeFacturaAsync(factura);
            result.Success = true;
            result.Message = $"Se agregó correctamente la factura a la persona con el ID {newFactura.PersonaId}";
            result.Data = null;

            return Ok(result);
        }
    }
}
