using Proyecto_API.Modelos.Dto;

namespace Proyecto_API.Data
{
    public static class InstrumentosStore
    {
        public static List<InstrumentosDto> instrumentosList = new List<InstrumentosDto>()
        {
            new InstrumentosDto { Id = 1,Nombre="Guitarra",Descripcion="Guitarra color café con cuerdas de metal"},
            new InstrumentosDto { Id = 2,Nombre="Saxofon",Descripcion="Saxofon alto color negro Yamaha"}
            
        };    
    }
}
