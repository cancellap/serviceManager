using SM.Domaiin.Entities.Base;

namespace SM.Domaiin.Entities
{
    public class Endereco : BaseEntity
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
     
        public string Cep { get; set; }
    }
}
