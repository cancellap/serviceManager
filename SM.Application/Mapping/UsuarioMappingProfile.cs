using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

        }
    }

}

