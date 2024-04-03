using Managment.core.Repositories.Facturas.Interfaces;
using Managment.core.Repositories.Facturas.Models;
using Managment.core.Repositories.Personas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Managment.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasRepository _facturasRepository;

        public FacturasController(IFacturasRepository facturasRepository)
        {
            _facturasRepository = facturasRepository;
        }

        [HttpGet("{PersonaId}")]
        public ActionResult<IEnumerable<Factura>> FindByPersonaId(int PersonaId)
        {
            var personas = this._facturasRepository.findFacturasByPersona(PersonaId);
            return Ok(personas);
        }

        [HttpPost]
        public ActionResult<Persona> StorePersona([FromBody] FacturaDto newFactura)
        {
            Factura factura = new Factura()
            {
                PersonaId = newFactura.PersonaId,
                Monto = newFactura.Monto,
                FechaEmision = DateTime.Now,
            };

            this._facturasRepository.storeFactura(factura);
            return Ok();
        }
    }
}
