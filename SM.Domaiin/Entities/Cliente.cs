using SM.Domaiin.Entities.Base;
using SM.Domaiin.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public class Cliente : BaseEntity
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        [InverseProperty("Cliente")]
        public EnderecoComplemento? EnderecoComplemento { get; set; }
        public List<Servicos>? Servicos { get; set; }
       
    }
}
