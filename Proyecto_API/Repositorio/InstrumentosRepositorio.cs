using Proyecto_API.Data;
using Proyecto_API.Modelos;
using Proyecto_API.Repositorio.IRepositorio;
namespace Proyecto_API.Repositorio
{
    public class InstrumentosRepositorio: Repositorio<instrumentos>, IInstrumentosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public InstrumentosRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
        public async Task<instrumentos> Actualizar (instrumentos entidad)
        {
            entidad.fecha_actualizacion = DateTimeOffset.Now;
            _db.instrumentos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
