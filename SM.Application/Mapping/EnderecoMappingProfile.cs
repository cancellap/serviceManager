using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class EnderecoMappingProfile : Profile
    {
        public EnderecoMappingProfile()
        {
            CreateMap<Endereco, EnderecoDto>();
        }
    }
}