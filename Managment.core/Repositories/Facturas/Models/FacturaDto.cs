using System.ComponentModel.DataAnnotations;

namespace Managment.core.Repositories.Facturas.Models
{
    public class FacturaDto
    {
        [Required]
        public int PersonaId { get; set; } // Clave foránea que referencia a Persona
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public DateTime FechaEmision { get; set; }
    }
}
