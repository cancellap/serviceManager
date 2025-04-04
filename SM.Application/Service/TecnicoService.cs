using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class TecnicoService(
        IEnderecoComplementoService enderecoComplementoService,
        IEnderecoService enderecoService,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ITecnicoService
    {
        private readonly IEnderecoComplementoService _enderecoComplementoService = enderecoComplementoService;
        private readonly IEnderecoService _enderecoService = enderecoService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<TecnicoDto> CreateTecnicoAsync(TecnicoCreateDto tecnicoCreateDto)
        {
            var tecnicoExistente = await _unitOfWork.TecnicoRepository.GetTecnicoByCpfAsync(tecnicoCreateDto.Cpf);
            if (tecnicoExistente != null)
                throw new Exception("Técnico já cadastrado");

            var tecnicoEntity = _mapper.Map<Tecnico>(tecnicoCreateDto);
            tecnicoEntity.EnderecoComplemento = null;

            var endereco = tecnicoCreateDto.EnderecoComplementoCreateDto.Endereco;
            int enderecoId = await _enderecoService.ObterOuCriarEnderecoAsync(endereco);

            if (enderecoId == 0)
                throw new InvalidOperationException("Erro ao obter ou criar o endereço.");

            var tecnicoCriado = await _unitOfWork.TecnicoRepository.AddAsync(tecnicoEntity);
            await _unitOfWork.CommitAsync();

            var enderecoComplemento = new EnderecoComplemento
            {
                TecnicoId = tecnicoCriado.Id,
                EnderecoId = enderecoId,
                Complemento = tecnicoCreateDto.EnderecoComplementoCreateDto.Complemento,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _enderecoComplementoService.CreateEnderecoComplementoAsync(enderecoComplemento);

            tecnicoCriado.EnderecoComplemento = enderecoComplemento;

            await _unitOfWork.TecnicoRepository.updateTecnicoAsync(tecnicoCriado);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TecnicoDto>(tecnicoCriado);
        }

        public async Task<IEnumerable<TecnicoDto>> GetAllTecnicoAsync()
        {
            var listaTecnicos = await _unitOfWork.TecnicoRepository.GetAllTecnicosAsync();
            return listaTecnicos.Count == 0 ? null : _mapper.Map<IEnumerable<TecnicoDto>>(listaTecnicos);
        }

        public async Task<TecnicoDto> GetTecnicoByIdAsync(int id)
        {
            var tecnico = await _unitOfWork.TecnicoRepository.GetTecnicoByIdAsync(id);
            return tecnico == null ? null : _mapper.Map<TecnicoDto>(tecnico);
        }

        public async Task<TecnicoDto> GetTecnicoByCpfAsync(string cpf)
        {
            var tecnico = await _unitOfWork.TecnicoRepository.GetTecnicoByCpfAsync(cpf);
            return tecnico == null ? null : _mapper.Map<TecnicoDto>(tecnico);
        }

        public async Task<TecnicoDto> DeleteTecnicoAsync(int id)
        {
            var tecnico = await _unitOfWork.TecnicoRepository.GetTecnicoByIdAsync(id);
            if (tecnico == null)
                throw new Exception("Técnico não encontrado");

            var enderecoSede = tecnico.EnderecoComplemento;

            var tecnicoExcluido = await _unitOfWork.TecnicoRepository.DeleteAsync(tecnico);
            await _enderecoComplementoService.DeleteAsync(enderecoSede.Id);

            await _unitOfWork.CommitAsync();
            return _mapper.Map<TecnicoDto>(tecnicoExcluido);
        }
    }
}
