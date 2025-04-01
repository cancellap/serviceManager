using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class EnderecoComplementoService(IMapper mapper, IEnderecoComplementoRepository enderecoComplementoRepository) : IEnderecoComplementoService
    {
        private readonly IEnderecoComplementoRepository _enderecoComplementoRepository = enderecoComplementoRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<EnderecoComplementoDto> CreateEnderecoComplementoAsync(EnderecoComplemento enderecoComplemento)
        {
            var enderecoComplementoCriado = await _enderecoComplementoRepository.AddAsync(enderecoComplemento);
            return _mapper.Map<EnderecoComplementoDto>(enderecoComplementoCriado);
        }

        public async Task<EnderecoComplementoDto> DeleteAsync(int id)
        {
            var enderecoComplemento = await _enderecoComplementoRepository.GetByIdEnderecoComplementoAsync(id);
            if (enderecoComplemento == null)
                throw new InvalidOperationException("EnderecoComplemento não encontrado");
            enderecoComplemento.IsDeleted = true;
            enderecoComplemento.DeletedAt = DateTime.UtcNow;
            await _enderecoComplementoRepository.UpdateEnderecoComplementoAsync(enderecoComplemento);
            return _mapper.Map<EnderecoComplementoDto>(enderecoComplemento);
        }

        public async Task<EnderecoComplementoDto> GetEnderecoComplementoByIdAsync(int id)
        {
            var enderecoComplemento = await _enderecoComplementoRepository.GetByIdEnderecoComplementoAsync(id);
            if (enderecoComplemento == null)
                throw new InvalidOperationException("EnderecoComplemento não encontrado");
            return _mapper.Map<EnderecoComplementoDto>(enderecoComplemento);
        }
    }
}
