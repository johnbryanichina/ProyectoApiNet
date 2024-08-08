using Proyecto_API.Modelos.Dto;

namespace Proyecto_API.Data
{
    public static class InstrumentosStore
    {
        public static List<InstrumentosDto> instrumentosList = new List<InstrumentosDto>()
        {
            new InstrumentosDto { id = 1,nombre="Guitarra",descripcion="Guitarra color café con cuerdas de metal"},
            new InstrumentosDto { id = 2,nombre="Saxofon",descripcion="Saxofon alto color negro Yamaha"}
            
        };    
    }
}
