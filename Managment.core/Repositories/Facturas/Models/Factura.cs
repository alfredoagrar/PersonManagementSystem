namespace Managment.core.Repositories.Facturas.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int PersonaId { get; set; } // Clave foránea que referencia a Persona
        public decimal Monto { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
