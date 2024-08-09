using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API.Modelos
{
    public class numero_instrumentos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
       public int instrumento_no { get; set; }
        [Required]
        public int instrumento_id { get; set; }
        [ForeignKey("instrumento_id")]
        public instrumentos instrumentos { get; set; }
        public string descripcion_instrumento { get; set; }
        public DateTimeOffset fecha_creacion { get; set; }
        public DateTimeOffset fecha_actualizacion { get; set; }
    }
}
