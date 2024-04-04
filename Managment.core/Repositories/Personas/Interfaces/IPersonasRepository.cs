using Managment.core.Repositories.Personas.Models;

namespace Managment.core.Repositories.Personas.Interfaces
{
    internal abstract class PersonasBase : IPersonasRepository
    {
        public abstract Task<bool> deletePersonaByIdentificacion(int id);
        public abstract Task<Persona?> findPersonaByIdentificacion(string id);
        public abstract Task<IEnumerable<Persona>> findPersonas();
        public abstract Task storePersona(Persona persona);
    }

    public interface IPersonasRepository
    {
        Task<Persona?> findPersonaByIdentificacion(string id);
        Task<IEnumerable<Persona>> findPersonas();
        Task<bool> deletePersonaByIdentificacion(int id);
        Task storePersona(Persona persona);
    }
}
