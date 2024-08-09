using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API.Modelos
{
    public class instrumentos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get;set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string imagen_url { get; set; }
        public DateTimeOffset fecha_creacion { get; set; }
        public DateTimeOffset fecha_actualizacion { get; set; }
    }
}
