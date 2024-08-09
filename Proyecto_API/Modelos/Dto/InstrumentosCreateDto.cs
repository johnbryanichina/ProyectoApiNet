using System.ComponentModel.DataAnnotations;

namespace Proyecto_API.Modelos.Dto
{
    public class InstrumentosCreateDto
    {
        
        [Required]
        [MaxLength(50)]

        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string imagenUrl { get; set; }
       
    }
}
