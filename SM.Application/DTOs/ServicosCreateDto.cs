namespace SM.Application.DTOs
{
    public class ServicosCreateDto
    {
        public string Descricao { get; set; }
        public List<int> TecnicosIds { get; set; }
    }
}
