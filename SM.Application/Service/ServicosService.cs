using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Repositories;

namespace SM.Application.Service
{
    public class ServicosService(IServicosRepository servicosRepository, IMapper mapper, RabbitMQProducer rabbitMQProducer, ITecnicoService tecnicoService) : IServicosService
    {
        private readonly IServicosRepository _servicosRepository = servicosRepository;
        private readonly IMapper _mapper = mapper;
        private readonly RabbitMQProducer _rabbitMQProducer = rabbitMQProducer;
        private readonly ITecnicoService _tecnicoService = tecnicoService;

        public async Task<ServicosDto> CreateServicoAsync(ServicosCreateDto servicosCreateDto, int idCliente)
        {
            var servicoEntity = new Servicos
            {
                Descricao = servicosCreateDto.Descricao,
                ClienteId = idCliente,
                IsAtivo = true,
                //servicoTecnicos = servicosCreateDto.TecnicosIds.Select(id => new ServicoTecnico { TecnicoId = id }).ToList(),
                servicoTecnicos = null,
                Id = 0
            };

            var servicoCriado = await _servicosRepository.CreateServicoAsync(servicoEntity);

            List<ServicoTecnico> servicoTecnicosList = [];


            foreach (var tecnicoId in servicosCreateDto.TecnicosIds)
            {
                var tecnico = await _tecnicoService.GetTecnicoByIdAsync(tecnicoId);
                if (tecnico.IsDeleted == true)
                {
                    throw new InvalidOperationException($"O técnico com ID {tecnico.Id} está com inativo no banco de dados do sistema.");
                }

                var servicoTecnicoExistente = await _servicosRepository.GetByIdsServicoTecnico(tecnicoId);

                if (servicoTecnicoExistente != null)
                {
                    throw new InvalidOperationException($"O técnico com ID {servicoTecnicoExistente.TecnicoId} já está associado a outro serviço.");
                }

                var servicoTecnico = new ServicoTecnico
                {
                    ServicoId = servicoCriado.Id,
                    TecnicoId = tecnicoId
                };
                servicoTecnicosList.Add(servicoTecnico);
                await _servicosRepository.CreateServicoTecnicoAsync(servicoTecnico);
            }
            var servicoteste = await _servicosRepository.FindOneId(servicoCriado.Id);
            return _mapper.Map<ServicosDto>(servicoteste);

        }
        public async Task<ServicosDto> GetServicoByIdAsync(int id)
        {
            var servico = await _servicosRepository.FindOneId(id);
            if (servico == null)
                return null;
            return _mapper.Map<ServicosDto>(servico);
        }

        public async Task<ServicosDto> AddTecnicoAoServico(int idServico, int idTecnico)
        {
            var servico = await _servicosRepository.FindOneId(idServico);
            var tecnico = await _tecnicoService.GetTecnicoByIdAsync(idTecnico);

            if (servico == null || tecnico == null)
                return null;

            //servico.servicoTecnicos.Add(new ServicoTecnico { ServicoId = idServico, TecnicoId = idTecnico });

            await _servicosRepository.CreateServicoTecnicoAsync(new ServicoTecnico { ServicoId = idServico, TecnicoId = idTecnico });
            await _servicosRepository.UpdateServicoAsync(servico);

            return _mapper.Map<ServicosDto>(servico);
        }
        public async Task<List<ServicosDto>> GetAllServicosAsync()
        {
            var servicos = await _servicosRepository.GetAllAsync();

            if (servicos.Count == 0)
                return null;

            var servicosDto = _mapper.Map<List<ServicosDto>>(servicos);
            return servicosDto;
        }
        public async Task<ServicosDto> DeleteServicoAsync(int id)
        {
            var servico = await _servicosRepository.DeleteServicoAsync(id);
            if (servico == null)
                return null;
            return _mapper.Map<ServicosDto>(servico);
        }

        public async Task<List<ServicosDto>> GetServicosWithFilterAsync(ServicoFiltro filtro)
        {
            var servicos = await _servicosRepository.GetServicosWithFilterAsync(filtro);
            return _mapper.Map<List<ServicosDto>>(servicos);
        }
    }
}
