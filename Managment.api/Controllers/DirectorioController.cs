using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models; // Asegúrate de tener un using para el modelo Persona
using Microsoft.AspNetCore.Mvc;

namespace Managment.api.Controllers
{
    [ApiController]
    [Route("[controller]/personas")]
    public class DirectorioController : ControllerBase
    {
        private readonly IPersonasRepository _personasRepository;

        public DirectorioController(IPersonasRepository personasRepository)
        {
            _personasRepository = personasRepository;
        }

        // GET: directorio/findPersonaByIdentificacion/5
        [HttpGet("{Identification}")]
        public ActionResult<Persona> FindPersonaByIdentificacion(int Identification)
        {
            var persona = _personasRepository.findPersonaByIdentificacion(Identification);
            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // GET: directorio/findPersonas
        [HttpGet]
        public ActionResult<IEnumerable<Persona>> FindPersonas()
        {
            var personas = _personasRepository.findPersonas();
            return Ok(personas);
        }

        // DELETE: directorio/deletePersonaByIdentificacion/5
        [HttpDelete("{Identification}")]
        public IActionResult DeletePersonaByIdentificacion(int Identification)
        {
            _personasRepository.deletePersonaByIdentificacion(Identification);
            return NoContent(); // 204 No Content es comúnmente utilizado para indicar que la operación fue exitosa pero no hay contenido para devolver.
        }

        // POST: directorio/storePersona
        [HttpPost]
        public ActionResult<Persona> StorePersona([FromBody] PersonaDto newPersona)
        {
            Persona persona = new Persona()
            {
                Nombre = newPersona.Nombre,
                ApellidoPaterno = newPersona.ApellidoPaterno,
                ApellidoMaterno = newPersona.ApellidoMaterno,
                Identificacion = newPersona.Identificacion,
            };
            _personasRepository.storePersona(persona);
            return Ok();
            // Asumiendo que el modelo Persona tiene una propiedad Identificacion
        }
    }
}
