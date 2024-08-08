using System.ComponentModel.DataAnnotations;

namespace Proyecto_API.Modelos.Dto
{
    public class InstrumentosUpdateDto
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]

        public string nombre { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public double precio { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public string imagenUrl { get; set; }
       
    }
}
