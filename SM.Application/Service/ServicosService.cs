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
        private readonly RabbitMQProducer _rabbitMQProducer;
        private readonly TecnicoRepository _tecnicoRepository;
        public ServicosService(ServicosRepository servicosRepository, IMapper mapper, RabbitMQProducer rabbitMQProducer, TecnicoRepository tecnicoRepository)
        {
            _servicosRepository = servicosRepository;
            _mapper = mapper;
            _rabbitMQProducer = rabbitMQProducer;
            _tecnicoRepository = tecnicoRepository;
        }

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

            //var servicoTecnico = new ServicoTecnico
            //{
            //    ServicoId = servicoCriado.Id,
            //    TecnicoId = 1
            //}

            //var mensagem = new ServicoMessageRabbitMQ
            //{
            //    ServicoId = servicoEntity.Id,
            //    DescricaoServico = servicoEntity.Descricao,
            //    NomeCliente = servicoEntity.Cliente.RazaoSocial,
            //    EmailCliente = servicoEntity.Cliente.Email,
            //    EmailsTecnicos = servicoEntity.servicoTecnicos.Select(st => st.Tecnico.Email).ToList()
            //};
            //_rabbitMQProducer.SendMessage(mensagem);
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
            var tecnico = await _tecnicoRepository.FindOneId(idTecnico);

            if (servico == null || tecnico == null)
                return null;

            //servico.servicoTecnicos.Add(new ServicoTecnico { ServicoId = idServico, TecnicoId = idTecnico });

            await _servicosRepository.CreateServicoTecnicoAsync(new ServicoTecnico { ServicoId = idServico, TecnicoId = idTecnico });
            await _servicosRepository.UpdateServicosAsync(servico);

            return _mapper.Map<ServicosDto>(servico);
        }
        public async Task<IEnumerable<ServicosDto>> GetAllServicosAsync()
        {
            var servicos = await _servicosRepository.GetAllAsync();
            if (servicos == null)
                return null;

            var servicosDto = _mapper.Map<IEnumerable<ServicosDto>>(servicos);
            return servicosDto;
        }
        public async Task<ServicosDto> DeleteServicoAsync(int id)
        {
            var servico = await _servicosRepository.DeleteServicoAsync(id);
            if (servico == null)
                return null;
            return _mapper.Map<ServicosDto>(servico);
        }

    }
}
