using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public class UsuarioRole
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}