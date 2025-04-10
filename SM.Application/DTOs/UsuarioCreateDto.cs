using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.DTOs
{
    public class UsuarioCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
