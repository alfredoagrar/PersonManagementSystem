using Managment.api.Models;
using Managment.core.Repositories.Facturas.Interfaces;
using Managment.core.Repositories.Facturas.Models;
using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Managment.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly IPersonasRepository _personasRepository;

        public FacturasController(IFacturasRepository facturasRepository, IPersonasRepository personasRepository)
        {
            _facturasRepository = facturasRepository;
            _personasRepository = personasRepository;
        }

        [HttpGet("{PersonaId}")]
        public async Task<ActionResult<IEnumerable<Factura>>> FindByPersonaIdAsync(int PersonaId)
        {
            IEnumerable<Factura> facturas = await this._facturasRepository.findFacturasByPersonaAsync(PersonaId);
            ApiResponse<IEnumerable<Factura>> result = new ApiResponse<IEnumerable<Factura>>();
            result.Success = true;
            result.Message = facturas.Any() ? "Info correctly executed" : "There is not info to be retrieved.";
            result.Data = facturas;
            return Ok(result);
        }

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
            result.Message = $"Se agrego correctamente la factura a la persona con el ID {newFactura.PersonaId}";
            result.Data = null;

            return Ok(result);
        }
    }
}
