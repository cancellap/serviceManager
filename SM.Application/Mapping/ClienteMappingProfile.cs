using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<ClienteCreateDto, Cliente>()
                .ForMember(dest => dest.EnderecoComplemento, opt => opt.MapFrom(src => src.EnderecoComplementoCreateDto));

            CreateMap<Cliente, ClienteDto>()
                .ForMember(dest => dest.EnderecoComplementoDto, opt => opt.MapFrom(src => src.EnderecoComplemento))
                .ForMember(dest => dest.ServicosDto, opt => opt.MapFrom(src => src.Servicos))
                .ForMember(dest => dest.ServicosDto, opt => opt.Ignore());

            CreateMap<Cliente, ClienteSemEnderecoInfosDbDto>();
            CreateMap<ClienteSemEnderecoInfosDbDto, Cliente>();
        }
    }
}