using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Infra.Repositories;

namespace SM.Application.Service
{
    public class ServicosService : IServicosService
    {
        private readonly ServicosRepository _servicosRepository;
        private readonly IMapper _mapper;
        public ServicosService(ServicosRepository servicosRepository, IMapper mapper)
        {
            _servicosRepository = servicosRepository;
            _mapper = mapper;
        }

        public async Task<ServicosDto> CreateServicoAsync(ServicosCreateDto servicosCreateDto, int idCliente)
        {
            var servicoEntity = new Servicos
            {
                Descricao = servicosCreateDto.Descricao,
                ClienteId = idCliente,
                IsAtivo = true,
                servicoTecnicos = servicosCreateDto.TecnicosIds.Select(id => new ServicoTecnico { TecnicoId = id }).ToList()
            };

            await _servicosRepository.AddAsync(servicoEntity);
            return _mapper.Map<ServicosDto>(servicoEntity);
        }
        public async Task<ServicosDto> GetServicoByIdAsync(int id)
        {
            var servico = await  _servicosRepository.FindOneId(id);
            if (servico == null)
                return null;
            return _mapper.Map<ServicosDto>(servico);
        }

        public async Task<ServicosDto> DeleteServicoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServicosDto>> GetAllServicosAsync()
        {
            throw new NotImplementedException();
        }

    }
}
