using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class ServicoMappingProfile : Profile
    {
        public ServicoMappingProfile()
        {
            CreateMap<Servicos, ServicosDto>()
                .ForMember(dest => dest.Tecnicos, opt => opt.MapFrom(src => src.servicoTecnicos))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
                .ReverseMap();

            CreateMap<ServicoTecnico, ServicoTecnicoDto>()
                .ForMember(dest => dest.TecnicoDto, opt => opt.MapFrom(src => src.Tecnico))
                .ReverseMap()
                .ForPath(src => src.Servico, opt => opt.Ignore());
        }
    }
}