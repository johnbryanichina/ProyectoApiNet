using System.ComponentModel.DataAnnotations;

namespace Proyecto_API.Modelos.Dto
{
    public class InstrumentosDto
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]

        public string nombre { get; set; }

        public string descripcion { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string imagen_url { get; set; }
       
    }
}
