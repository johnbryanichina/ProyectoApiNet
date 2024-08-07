using System.ComponentModel.DataAnnotations;

namespace Proyecto_API.Modelos.Dto
{
    public class InstrumentosDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]

        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
    }
}
