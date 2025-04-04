using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Domaiin.Validation;

namespace SM.Application.Service
{
    public class ClienteService(IMapper mapper, IUnitOfWork unitOfWork, IEnderecoComplementoService enderecoComplementoService, IEnderecoService enderecoService) : IClienteService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEnderecoComplementoService _enderecoComplementoService = enderecoComplementoService;
        private readonly IEnderecoService _enderecoService = enderecoService;

        public async Task<ClienteDto> CreateClienteAsync(ClienteCreateDto clienteCreateDto)
        {
            var clienteExistente = await _unitOfWork.ClienteRepository.GetClienteByCnpjAsync(clienteCreateDto.Cnpj);
            if (clienteExistente != null)
                throw new InvalidOperationException("Cliente já cadastrado");

            if (ValidaCnpj.IsCnpj(clienteCreateDto.Cnpj))
                throw new InvalidOperationException("CNPJ inválido");

            var clienteEntity = _mapper.Map<Cliente>(clienteCreateDto);
            clienteEntity.EnderecoComplemento = null;

            var endereco = clienteCreateDto.EnderecoComplementoCreateDto.Endereco;
            int enderecoId = await _enderecoService.ObterOuCriarEnderecoAsync(endereco);
            if (enderecoId == 0)
                throw new InvalidOperationException("Erro ao obter ou criar o endereço.");

            var clienteCriado = await _unitOfWork.ClienteRepository.AddAsync(clienteEntity);
            await _unitOfWork.CommitAsync();

            var enderecoComplemento = new EnderecoComplemento
            {
                ClienteId = clienteCriado.Id,
                EnderecoId = enderecoId,
                Complemento = clienteCreateDto.EnderecoComplementoCreateDto.Complemento,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _enderecoComplementoService.CreateEnderecoComplementoAsync(enderecoComplemento);
            await _unitOfWork.CommitAsync();

            clienteCriado.EnderecoComplemento = enderecoComplemento;

            return _mapper.Map<ClienteDto>(clienteCriado);
        }

        public async Task<ClienteDto> DeleteClienteAsync(int id)
        {
            var cliente = await _unitOfWork.ClienteRepository.GetByIdClientesAsync(id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            var enderecoComplemento = cliente.EnderecoComplemento;

            var clienteExcluido = await _unitOfWork.ClienteRepository.DeleteAsync(cliente);
            await _unitOfWork.CommitAsync();

            await _enderecoComplementoService.DeleteAsync(enderecoComplemento.Id);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ClienteDto>(clienteExcluido);
        }

        public async Task<IEnumerable<ClienteDto>> GetAllClientesAsync()
        {
            var listaCliente = await _unitOfWork.ClienteRepository.GetAllClientesAsync();
            if (listaCliente.Count == 0)
                return null;

            return _mapper.Map<IEnumerable<ClienteDto>>(listaCliente);
        }

        public async Task<ClienteDto> GetClienteByCnpjAsync(string cnpj)
        {
            var cliente = await _unitOfWork.ClienteRepository.GetClienteByCnpjAsync(cnpj);
            if (cliente == null)
                return null;

            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> GetClienteByIdAsync(int id)
        {
            var cliente = await _unitOfWork.ClienteRepository.GetByIdClientesAsync(id);
            if (cliente == null)
                return null;

            return _mapper.Map<ClienteDto>(cliente);
        }
    }
}
