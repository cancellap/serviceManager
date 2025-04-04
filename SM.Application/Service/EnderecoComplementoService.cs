using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class EnderecoComplementoService(IMapper mapper, IUnitOfWork unitOfWork) : IEnderecoComplementoService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<EnderecoComplementoDto> CreateEnderecoComplementoAsync(EnderecoComplemento enderecoComplemento)
        {
            var enderecoComplementoCriado = await _unitOfWork.EnderecoComplementoRepository.AddAsync(enderecoComplemento);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EnderecoComplementoDto>(enderecoComplementoCriado);
        }

        public async Task<EnderecoComplementoDto> DeleteAsync(int id)
        {
            var enderecoComplemento = await _unitOfWork.EnderecoComplementoRepository.GetByIdEnderecoComplementoAsync(id);
            if (enderecoComplemento == null)
                throw new InvalidOperationException("EnderecoComplemento não encontrado");

            enderecoComplemento.IsDeleted = true;
            enderecoComplemento.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.EnderecoComplementoRepository.UpdateEnderecoComplementoAsync(enderecoComplemento);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<EnderecoComplementoDto>(enderecoComplemento);
        }

        public async Task<EnderecoComplementoDto> GetEnderecoComplementoByIdAsync(int id)
        {
            var enderecoComplemento = await _unitOfWork.EnderecoComplementoRepository.GetByIdEnderecoComplementoAsync(id);
            if (enderecoComplemento == null)
                throw new InvalidOperationException("EnderecoComplemento não encontrado");

            return _mapper.Map<EnderecoComplementoDto>(enderecoComplemento);
        }
    }
}
