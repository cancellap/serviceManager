using AutoMapper.Configuration.Annotations;
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SM.Application.DTOs
{
    public class UsuarioRoleDto
    {
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }

        public string RoleNome
        {
            get
            {
                return Role != null ? Role.Nome : string.Empty;
            }
        }
    }
}
