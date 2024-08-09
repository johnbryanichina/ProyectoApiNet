using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API.Modelos.Dto
{
    public class NumeroInstrumentoUpdateDto
    {
        [Required]
        public int instrumento_no { get; set; }
        [Required]
        public int instrumento_id { get; set; } 
        public string descripcion_instrumento { get; set; }

    }
}
