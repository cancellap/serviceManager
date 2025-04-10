using SM.Domaiin.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public class Usuario : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UsuarioRole> UsuarioRoles { get; set; }
    }
}
