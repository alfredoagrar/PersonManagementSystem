using Managment.core.Repositories.Personas.Models;

namespace Managment.core.Repositories.Personas.Interfaces
{
    internal abstract class PersonasBase : IPersonasRepository
    {
        public abstract void deletePersonaByIdentificacion(int id);
        public abstract Persona findPersonaByIdentificacion(int id);
        public abstract IEnumerable<Persona> findPersonas();
        public abstract void storePersona(Persona persona);
    }

    public interface IPersonasRepository
    {
        Persona findPersonaByIdentificacion(int id);
        IEnumerable<Persona> findPersonas();
        void deletePersonaByIdentificacion(int id);
        void storePersona(Persona persona);
    }
}
