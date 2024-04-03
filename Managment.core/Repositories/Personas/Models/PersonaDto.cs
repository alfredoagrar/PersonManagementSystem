using System.ComponentModel.DataAnnotations;

namespace Managment.core.Repositories.Personas.Models
{
    public class PersonaDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        [Required]
        public string Identificacion { get; set; }
    }
}
