using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class TecnicoMappingProfile : Profile
    {
        public TecnicoMappingProfile()
        {
            CreateMap<TecnicoCreateDto, Tecnico>()
                .ForMember(dest => dest.EnderecoComplemento, opt => opt.MapFrom(src => src.EnderecoComplementoCreateDto));

            CreateMap<Tecnico, TecnicoDto>()
                .ForMember(dest => dest.EnderecoComplementoDto, opt => opt.MapFrom(src => src.EnderecoComplemento));

            CreateMap<TecnicoDto, Tecnico>();
            CreateMap<Tecnico, TecnicoSemEnderecoInfosDbDto>();
        }
    }
}