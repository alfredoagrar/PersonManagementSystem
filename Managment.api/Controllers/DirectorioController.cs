using Managment.api.Models;
using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Managment.api.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con el directorio de personas.
    /// </summary>
    [ApiController]
    [Route("[controller]/personas")]
    public class DirectorioController : ControllerBase
    {
        private readonly IPersonasRepository _personasRepository;

        /// <summary>
        /// Constructor de la clase DirectorioController.
        /// </summary>
        /// <param name="personasRepository">Instancia de IPersonasRepository para acceder a los datos de las personas.</param>
        public DirectorioController(IPersonasRepository personasRepository)
        {
            _personasRepository = personasRepository;
        }

        /// <summary>
        /// Obtiene una persona por su identificación.
        /// </summary>
        /// <param name="Identification">Identificación de la persona.</param>
        /// <returns>ActionResult con la persona encontrada.</returns>
        [HttpGet("{Identification}")]
        public async Task<ActionResult<Persona?>> FindPersonaByIdentificacionAsync(string Identification)
        {
            Persona? persona = await _personasRepository.findPersonaByIdentificacion(Identification);

            // Usar el constructor o inicialización directa para asignar valores
            ApiResponse<Persona?> result = new ApiResponse<Persona?>
            {
                Success = true,
                Message = persona == null ? $"No hay persona con la identificación {Identification}" : null,
                Data = persona
            };

            return Ok(result);

        }

        /// <summary>
        /// Obtiene todas las personas.
        /// </summary>
        /// <returns>ActionResult con la lista de personas.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> FindPersonasAsync()
        {
            IEnumerable<Persona> personas = await _personasRepository.findPersonas();
            ApiResponse<IEnumerable<Persona>> result = new ApiResponse<IEnumerable<Persona>>();
            result.Success = true;
            result.Message = personas.Any() ? "Información ejecutada correctamente" : "No hay información para recuperar.";
            result.Data = personas;
            return Ok(result);
        }

        /// <summary>
        /// Elimina una persona por su Id.
        /// </summary>
        /// <param name="Id">Id de la persona a eliminar.</param>
        /// <returns>ActionResult indicando si la eliminación fue exitosa.</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePersonaByIdentificacionAsync(int Id)
        {
            bool isDeleted = await _personasRepository.deletePersonaByIdentificacion(Id);

            string message = isDeleted
                ? $"La persona con el Id {Id} fue eliminada correctamente."
                : $"No hay una persona con el Id {Id}.";

            ApiResponse<string?> result = new ApiResponse<string?>
            {
                Success = true,
                Message = message,
                Data = null
            };

            return Ok(result);


        }

        /// <summary>
        /// Almacena una nueva persona.
        /// </summary>
        /// <param name="newPersona">Datos de la nueva persona.</param>
        /// <returns>ActionResult con la persona creada.</returns>
        [HttpPost]
        public async Task<ActionResult<Persona>> StorePersonaAsync([FromBody] PersonaDto newPersona)
        {

            Persona? IsUsedIdentification = await _personasRepository.findPersonaByIdentificacion(newPersona.Identificacion);
            ApiResponse<Persona?> result = new ApiResponse<Persona?>();
            if (IsUsedIdentification is not null) 
            {
                result.Success = true;
                result.Message = $"Ya existe una persona con la identificacion '{newPersona.Identificacion}'";
                result.Data = null;

                return Ok(result);
            }
            Persona persona = new Persona()
            {
                Nombre = newPersona.Nombre,
                ApellidoPaterno = newPersona.ApellidoPaterno,
                ApellidoMaterno = newPersona.ApellidoMaterno,
                Identificacion = newPersona.Identificacion,
            };
            await _personasRepository.storePersona(persona);

            Persona? createdPerson = await _personasRepository.findPersonaByIdentificacion(newPersona.Identificacion);
            result.Success = true;
            result.Message = null;
            result.Data = createdPerson;

            return Ok(result);
        }
    }
}
