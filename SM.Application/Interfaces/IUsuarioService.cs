using SM.Application.DTOs;
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> AddUser(UsuarioCreateDto usuarioCreateDto);
        Task RemoveUser(string username);
        Task<UsuarioDto> GetByUsername(string username);
        Task<Usuario> EditRolesOfUser(string username, List<string> roles);
    }
}
