using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Repositories;

namespace SM.Application.Service
{
    public class TecnicoService(IEnderecoComplementoService enderecoComplementoService, ITecnicoRepository tecnicoRepository, IMapper mapper, IEnderecoService enderecoService) : ITecnicoService
    {
        private readonly ITecnicoRepository _tecnicoRepository = tecnicoRepository;
        private readonly IEnderecoComplementoService _enderecoComplementoService = enderecoComplementoService;
        private readonly IMapper _mapper = mapper;
        private readonly IEnderecoService _enderecoService = enderecoService;

        public async Task<TecnicoDto> CreateTecnicoAsync(TecnicoCreateDto tecnicoCreateDto)
        {
            var tecnicoExistente = await _tecnicoRepository.GetTecnicoByCpfAsync(tecnicoCreateDto.Cpf);
            if (tecnicoExistente != null)
                throw new Exception("Tecnico já cadastrado");

            var tecnicoEntity = _mapper.Map<Tecnico>(tecnicoCreateDto);
            tecnicoEntity.EnderecoComplemento = null;

            var endereco = tecnicoCreateDto.EnderecoComplementoCreateDto.Endereco;

            int enderecoId = await _enderecoService.ObterOuCriarEnderecoAsync(endereco);
            if (enderecoId == 0)
                throw new InvalidOperationException("Erro ao obter ou criar o endereço.");

            var tecnicoCriado = await _tecnicoRepository.AddAsync(tecnicoEntity);

            var enderecoComplemento = new EnderecoComplemento
            {
                TecnicoId = tecnicoEntity.Id,
                EnderecoId = enderecoId,
                Complemento = tecnicoCreateDto.EnderecoComplementoCreateDto.Complemento,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _enderecoComplementoService.CreateEnderecoComplementoAsync(enderecoComplemento);
            tecnicoCriado.EnderecoComplemento = enderecoComplemento;

            await _tecnicoRepository.updateTecnicoAsync(tecnicoCriado);

            return _mapper.Map<TecnicoDto>(tecnicoCriado);
        }



        public async Task<IEnumerable<TecnicoDto>> GetAllTecnicoAsync()
        {
            var listaTecnicos = await _tecnicoRepository.GetAllTecnicosAsync();
            if (listaTecnicos.Count == 0)
                return null;
            var listaTecnicosDto = _mapper.Map<IEnumerable<TecnicoDto>>(listaTecnicos);
            return listaTecnicosDto;
        }

        public async Task<TecnicoDto> GetTecnicoByIdAsync(int id)
        {
            var tecnico = await _tecnicoRepository.GetTecnicoByIdAsync(id);
            if (tecnico == null)
                return null;
            var tecnicoDto = _mapper.Map<TecnicoDto>(tecnico);
            return tecnicoDto;
        }

        public async Task<TecnicoDto> GetTecnicoByCpfAsync(string cpf)
        {
            var tecnico = await _tecnicoRepository.GetTecnicoByCpfAsync(cpf);
            if (tecnico == null)
                return null;
            var tecnicoDto = _mapper.Map<TecnicoDto>(tecnico);
            return tecnicoDto;
        }

        public async Task<TecnicoDto> DeleteTecnicoAsync(int id)
        {
            var tecnico = await _tecnicoRepository.GetTecnicoByIdAsync(id);

            var enderecoSede = tecnico.EnderecoComplemento;

            if (tecnico == null)
                throw new Exception("Cliente não encontrado");

            var tecnicoExcluido = await _tecnicoRepository.DeleteAsync(tecnico);
            await _enderecoComplementoService.DeleteAsync(enderecoSede.Id);

            return _mapper.Map<TecnicoDto>(tecnicoExcluido);
        }
    }
}
