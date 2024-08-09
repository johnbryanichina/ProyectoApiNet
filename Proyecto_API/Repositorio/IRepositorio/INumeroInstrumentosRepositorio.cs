using Proyecto_API.Modelos;

namespace Proyecto_API.Repositorio.IRepositorio
{
    public interface INumeroInstrumentosRepositorio:IRepositorio<numero_instrumentos>
    {
        Task<numero_instrumentos> Actualizar(numero_instrumentos entidad);

    }
}
