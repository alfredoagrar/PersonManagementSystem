using Managment.core.Repositories.Personas.Models;

namespace Managment.core.Repositories.Personas.Interfaces
{
    /// <summary>
    /// Clase base abstracta para el repositorio de personas.
    /// </summary>
    internal abstract class PersonasBase : IPersonasRepository
    {
        /// <summary>
        /// Elimina una persona por su identificación.
        /// </summary>
        /// <param name="id">Identificación de la persona.</param>
        /// <returns>True si se eliminó la persona, de lo contrario False.</returns>
        public abstract Task<bool> deletePersonaByIdentificacion(int id);

        /// <summary>
        /// Busca una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>La persona encontrada o null si no se encontró ninguna.</returns>
        public abstract Task<Persona?> findPersonaById(int id);

        /// <summary>
        /// Busca una persona por su identificación.
        /// </summary>
        /// <param name="id">Identificación de la persona.</param>
        /// <returns>La persona encontrada o null si no se encontró ninguna.</returns>
        public abstract Task<Persona?> findPersonaByIdentificacion(string id);

        /// <summary>
        /// Obtiene todas las personas.
        /// </summary>
        /// <returns>Una colección de personas.</returns>
        public abstract Task<IEnumerable<Persona>> findPersonas();

        /// <summary>
        /// Almacena una persona.
        /// </summary>
        /// <param name="persona">La persona a almacenar.</param>
        public abstract Task storePersona(Persona persona);
    }

    /// <summary>
    /// Interfaz para el repositorio de personas.
    /// </summary>
    public interface IPersonasRepository
    {
        /// <summary>
        /// Busca una persona por su identificación.
        /// </summary>
        /// <param name="id">Identificación de la persona.</param>
        /// <returns>La persona encontrada o null si no se encontró ninguna.</returns>
        Task<Persona?> findPersonaByIdentificacion(string id);

        /// <summary>
        /// Obtiene todas las personas.
        /// </summary>
        /// <returns>Una colección de personas.</returns>
        Task<IEnumerable<Persona>> findPersonas();

        /// <summary>
        /// Elimina una persona por su identificación.
        /// </summary>
        /// <param name="id">Identificación de la persona.</param>
        /// <returns>True si se eliminó la persona, de lo contrario False.</returns>
        Task<bool> deletePersonaByIdentificacion(int id);

        /// <summary>
        /// Almacena una persona.
        /// </summary>
        /// <param name="persona">La persona a almacenar.</param>
        Task storePersona(Persona persona);

        /// <summary>
        /// Busca una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>La persona encontrada o null si no se encontró ninguna.</returns>
        Task<Persona?> findPersonaById(int id);
    }
}
