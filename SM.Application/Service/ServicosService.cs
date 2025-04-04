using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class ServicosService(IUnitOfWork unitOfWork, IMapper mapper, RabbitMQProducer rabbitMQProducer, ITecnicoService tecnicoService) : IServicosService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
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
                servicoTecnicos = null,
                Id = 0
            };

            var servicoCriado = await _unitOfWork.ServicosRepository.CreateServicoAsync(servicoEntity);
            await _unitOfWork.CommitAsync();

            List<ServicoTecnico> servicoTecnicosList = [];

            foreach (var tecnicoId in servicosCreateDto.TecnicosIds)
            {
                var tecnico = await _tecnicoService.GetTecnicoByIdAsync(tecnicoId);
                if (tecnico.IsDeleted)
                    throw new InvalidOperationException($"O técnico com ID {tecnico.Id} está inativo.");

                var servicoTecnicoExistente = await _unitOfWork.ServicosRepository.GetByIdsServicoTecnico(tecnicoId);
                if (servicoTecnicoExistente != null)
                    throw new InvalidOperationException($"O técnico com ID {servicoTecnicoExistente.TecnicoId} já está associado a outro serviço.");

                var servicoTecnico = new ServicoTecnico
                {
                    ServicoId = servicoCriado.Id,
                    TecnicoId = tecnicoId
                };

                servicoTecnicosList.Add(servicoTecnico);
                await _unitOfWork.ServicosRepository.CreateServicoTecnicoAsync(servicoTecnico);
            }

            await _unitOfWork.CommitAsync();

            var servicoteste = await _unitOfWork.ServicosRepository.FindOneId(servicoCriado.Id);
            return _mapper.Map<ServicosDto>(servicoteste);
        }

        public async Task<ServicosDto> GetServicoByIdAsync(int id)
        {
            var servico = await _unitOfWork.ServicosRepository.FindOneId(id);
            return servico == null ? null : _mapper.Map<ServicosDto>(servico);
        }

        public async Task<ServicosDto> AddTecnicoAoServico(int idServico, int idTecnico)
        {
            var servico = await _unitOfWork.ServicosRepository.FindOneId(idServico);
            var tecnico = await _tecnicoService.GetTecnicoByIdAsync(idTecnico);

            if (servico == null || tecnico == null)
                return null;

            await _unitOfWork.ServicosRepository.CreateServicoTecnicoAsync(new ServicoTecnico
            {
                ServicoId = idServico,
                TecnicoId = idTecnico
            });

            await _unitOfWork.ServicosRepository.UpdateServicoAsync(servico);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ServicosDto>(servico);
        }

        public async Task<List<ServicosDto>> GetAllServicosAsync()
        {
            var servicos = await _unitOfWork.ServicosRepository.GetAllAsync();
            return servicos.Count == 0 ? null : _mapper.Map<List<ServicosDto>>(servicos);
        }


        public async Task<ServicosDto> DeleteServicoAsync(int id)
        {
            // Busca e deleta o serviço
            var servico = await _unitOfWork.ServicosRepository.DeleteServicoAsync(id);
            if (servico == null) return null;

            // Busca todos os vínculos e remove
            var servicoTecnicos = await _unitOfWork.ServicosRepository.GetAllByServicoIdAsync(id);
            if (servicoTecnicos != null && servicoTecnicos.Any())
            {
                _unitOfWork.ServicosRepository.DeleteServicoTecnicos(servicoTecnicos);
            }

            await _unitOfWork.CommitAsync(); // Salva tudo em uma transação
            return _mapper.Map<ServicosDto>(servico);
        }



        public async Task<List<ServicosDto>> GetServicosWithFilterAsync(ServicoFiltro filtro)
        {
            var servicos = await _unitOfWork.ServicosRepository.GetServicosWithFilterAsync(filtro);
            return _mapper.Map<List<ServicosDto>>(servicos);
        }
    }
}
