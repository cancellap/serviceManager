using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Mapping
{
    public class UsuarioRoleMappingProfile : Profile
    {
        public UsuarioRoleMappingProfile()
        {
            CreateMap<UsuarioRole, UsuarioRoleDto>().ReverseMap();
        }
    }

}

