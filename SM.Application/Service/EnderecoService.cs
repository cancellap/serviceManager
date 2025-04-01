using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class EnderecoService(IEnderecoRepository enderecoRepository) : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;

        public async Task<int> ObterOuCriarEnderecoAsync(Endereco endereco)
        {
            if (endereco == null)
                return 0;

            var enderecoExistente = await _enderecoRepository.
                GetEnderecoByDetailsAsync(endereco.Rua, endereco.Cidade, endereco.Estado, endereco.Cep);

            if (enderecoExistente != null)
            {
                Console.WriteLine("Endereco já existe: " + enderecoExistente.Id);
                return enderecoExistente.Id;
            }

            await _enderecoRepository.AddAsync(endereco);

            Console.WriteLine("Endereco criado: " + endereco.Id);
            return endereco.Id;
        }
    }
}
