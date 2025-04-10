using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<int, string> Roles { get; set; }
    }
}
