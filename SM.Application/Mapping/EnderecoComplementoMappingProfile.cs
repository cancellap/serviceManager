using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class EnderecoComplementoMappingProfile : Profile
    {
        public EnderecoComplementoMappingProfile()
        {
            CreateMap<EnderecoComplementoDto, EnderecoComplemento>();

            CreateMap<EnderecoComplemento, EnderecoComplementoDto>()
                .ForMember(dest => dest.EnderecoDto, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.ClienteDto, opt => opt.Ignore())
                .ForMember(dest => dest.TecnicoDto, opt => opt.Ignore());

            CreateMap<EnderecoComplementoCreateDto, EnderecoComplemento>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Complemento));
        }
    }
}