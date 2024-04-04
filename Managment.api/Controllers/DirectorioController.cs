using Managment.api.Models;
using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models;
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

        [HttpGet("{Identification}")]
        public async Task<ActionResult<Persona?>> FindPersonaByIdentificacionAsync(string Identification)
        {
            Persona? persona = await _personasRepository.findPersonaByIdentificacion(Identification);
            ApiResponse<Persona?> result = new ApiResponse<Persona?>();
            if (persona == null)
            {
                result.Success = true;
                result.Message = $"No hay perdona con la identificacion {Identification}";
                result.Data = null;
                return Ok(result);
            }

            result.Success = true;
            result.Message = null;
            result.Data = persona;
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> FindPersonasAsync()
        {
            IEnumerable<Persona> personas = await _personasRepository.findPersonas();
            ApiResponse<IEnumerable<Persona>> result = new ApiResponse<IEnumerable<Persona>>();
            result.Success = true;
            result.Message = personas.Any() ? "Info correctly executed" : "There is not info to be retrieved.";
            result.Data = personas;
            return Ok(result);
        }

        [HttpDelete("{Identification}")]
        public async Task<IActionResult> DeletePersonaByIdentificacionAsync(int Id)
        {
            bool IsDeleted = await _personasRepository.deletePersonaByIdentificacion(Id);
            ApiResponse<string?> result = new ApiResponse<string?>();

            if(IsDeleted)
            {
                result.Success = true;
                result.Message = $"La persona con el Id {Id} fue eliminada correctamente.";
                result.Data = null;
                return Ok(result);
            }

            result.Success = true;
            result.Message = $"No hay una persona con la Id {Id}.";
            result.Data = null;
            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult<Persona>> StorePersonaAsync([FromBody] PersonaDto newPersona)
        {
            Persona persona = new Persona()
            {
                Nombre = newPersona.Nombre,
                ApellidoPaterno = newPersona.ApellidoPaterno,
                ApellidoMaterno = newPersona.ApellidoMaterno,
                Identificacion = newPersona.Identificacion,
            };
            await _personasRepository.storePersona(persona);

            Persona? createdPerson = await _personasRepository.findPersonaByIdentificacion(newPersona.Identificacion);
            ApiResponse<Persona?> result = new ApiResponse<Persona?>();
            result.Success = true;
            result.Message = null;
            result.Data = createdPerson;

            return Ok(result);
        }
    }
}
