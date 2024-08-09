using Proyecto_API.Modelos;

namespace Proyecto_API.Repositorio.IRepositorio
{
    public interface IInstrumentosRepositorio:IRepositorio<instrumentos>
    {
        Task<instrumentos> Actualizar(instrumentos entidad);

    }
}
