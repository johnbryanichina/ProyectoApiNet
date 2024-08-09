using Proyecto_API.Data;
using Proyecto_API.Modelos;
using Proyecto_API.Repositorio.IRepositorio;
namespace Proyecto_API.Repositorio
{
    public class NumeroInstrumentosRepositorio: Repositorio<numero_instrumentos>, INumeroInstrumentosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public NumeroInstrumentosRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
        public async Task<numero_instrumentos> Actualizar (numero_instrumentos entidad)
        {
            entidad.fecha_actualizacion = DateTimeOffset.UtcNow;
            _db.numero_instrumentos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
