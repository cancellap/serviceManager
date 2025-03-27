using SM.Domaiin.Entities;

namespace SM.Application.DTOs
{
    public class ServicosDto
    {
        public int Id { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Descricao { get; set; }
        public List<ServicoTecnicoDto> Tecnicos { get; set; }
        //public int ClienteId { get; set; }
        public ClienteSemEnderecoInfosDbDto Cliente{ get; set; }
    }
}
