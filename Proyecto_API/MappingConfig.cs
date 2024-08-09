using AutoMapper;
using Proyecto_API.Modelos;
using Proyecto_API.Modelos.Dto;

namespace Proyecto_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<instrumentos, InstrumentosDto>();
            CreateMap<InstrumentosDto, instrumentos>();

            CreateMap<instrumentos, InstrumentosCreateDto>().ReverseMap();
            CreateMap<instrumentos, InstrumentosUpdateDto>().ReverseMap();
        }
    }
}
