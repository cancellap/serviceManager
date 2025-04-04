using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class EnderecoService(IUnitOfWork unitOfWork) : IEnderecoService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> ObterOuCriarEnderecoAsync(Endereco endereco)
        {
            if (endereco == null)
                return 0;

            var enderecoExistente = await _unitOfWork.EnderecoRepository
                .GetEnderecoByDetailsAsync(endereco.Rua, endereco.Cidade, endereco.Estado, endereco.Cep);

            if (enderecoExistente != null)
            {
                Console.WriteLine("Endereco já existe: " + enderecoExistente.Id);
                return enderecoExistente.Id;
            }

            await _unitOfWork.EnderecoRepository.AddAsync(endereco);
            await _unitOfWork.CommitAsync();

            Console.WriteLine("Endereco criado: " + endereco.Id);
            return endereco.Id;
        }
    }
}
