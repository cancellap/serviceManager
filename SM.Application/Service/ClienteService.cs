﻿using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Domaiin.Validation;

namespace SM.Application.Service
{
    public class ClienteService(IClienteRepository clienteRepository, IEnderecoComplementoService enderecoComplementoService, IEnderecoService enderecoService, IMapper mapper) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IEnderecoComplementoService _enderecoComplementoService = enderecoComplementoService;
        private readonly IEnderecoService _enderecoService = enderecoService;
        private readonly IMapper _mapper = mapper;

        public async Task<ClienteDto> CreateClienteAsync(ClienteCreateDto clienteCreateDto)
        {
            var clienteExistente = await _clienteRepository.GetClienteByCnpjAsync(clienteCreateDto.Cnpj);
            if (clienteExistente != null)
                throw new InvalidOperationException("Cliente já cadastrado");

            if (ValidaCnpj.IsCnpj(clienteCreateDto.Cnpj))
            {
                throw new InvalidOperationException("CNPJ inválido");
            }

            var clienteEntity = _mapper.Map<Cliente>(clienteCreateDto);
            clienteEntity.EnderecoComplemento = null;

            var endereco = clienteCreateDto.EnderecoComplementoCreateDto.Endereco;
            int enderecoId = await _enderecoService.ObterOuCriarEnderecoAsync(endereco);
            if (enderecoId == 0)
                throw new InvalidOperationException("Erro ao obter ou criar o endereço.");

            var clienteCriado = await _clienteRepository.AddAsync(clienteEntity);
            clienteCriado.EnderecoComplemento = null;

            var enderecoComplemento = new EnderecoComplemento
            {
                ClienteId = clienteCriado.Id,
                EnderecoId = enderecoId,
                Complemento = clienteCreateDto.EnderecoComplementoCreateDto.Complemento,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            Console.WriteLine("EnderecoComplemento: " + enderecoComplemento.EnderecoId + "  -  " + enderecoComplemento.Id + "  -  " + enderecoComplemento.ClienteId);
            await _enderecoComplementoService.CreateEnderecoComplementoAsync(enderecoComplemento);

            clienteCriado.EnderecoComplemento = enderecoComplemento;
            await _clienteRepository.updateClienteAsync(clienteCriado);

            return _mapper.Map<ClienteDto>(clienteCriado);
        }

        public async Task<ClienteDto> DeleteClienteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdClientesAsync(id);

            var enderecoComplemento = cliente.EnderecoComplemento;

            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            var clienteExcluido = await _clienteRepository.DeleteAsync(cliente);
            await _enderecoComplementoService.DeleteAsync(enderecoComplemento.Id);

            return _mapper.Map<ClienteDto>(clienteExcluido);

        }

        public async Task<IEnumerable<ClienteDto>> GetAllClientesAsync()
        {
            var listaCliente = await _clienteRepository.GetAllClientesAsync();
            if (listaCliente.Count == 0)
                return null;
            var listaClientesDtos = _mapper.Map<IEnumerable<ClienteDto>>(listaCliente);
            return listaClientesDtos;
        }


        public async Task<ClienteDto> GetClienteByCnpjAsync(string cnpj)
        {
            var cliente = await _clienteRepository.GetClienteByCnpjAsync(cnpj);
            if (cliente == null)
                return null;
            var clienteDto = _mapper.Map<ClienteDto>(cliente);
            return clienteDto;
        }

        public async Task<ClienteDto> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdClientesAsync(id);
            if (cliente == null)
                return null;
            var clienteDto = _mapper.Map<ClienteDto>(cliente);
            return clienteDto;
        }
    }
}
