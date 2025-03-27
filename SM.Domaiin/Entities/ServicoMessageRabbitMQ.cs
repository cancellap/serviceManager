using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public  class ServicoMessageRabbitMQ
    {
        public int ServicoId { get; set; }
        public string DescricaoServico { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public List<string> EmailsTecnicos { get; set; }
    }
}
