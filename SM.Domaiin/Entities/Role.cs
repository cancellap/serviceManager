using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<UsuarioRole> UsuarioRoles { get; set; }
    }
}