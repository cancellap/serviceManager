using AutoMapper;
using Moq;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Application.Service;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using Xunit;

namespace ServeceManageTests.Tests.TestLayerApplication
{
    public class ClienteTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly Mock<IEnderecoComplementoService> _mockEnderecoComplementoService;
        private readonly Mock<IEnderecoService> _mockEnderecoService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClienteService _clienteService;

        public ClienteTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockClienteRepository = new Mock<IClienteRepository>();
            _mockEnderecoComplementoService = new Mock<IEnderecoComplementoService>();
            _mockEnderecoService = new Mock<IEnderecoService>();
            _mockMapper = new Mock<IMapper>();

            // setup para retornar o mock do IClienteRepository
            _mockUnitOfWork.Setup(u => u.ClienteRepository).Returns(_mockClienteRepository.Object);

            _clienteService = new ClienteService(
                _mockMapper.Object,
                _mockUnitOfWork.Object,
                _mockEnderecoComplementoService.Object,
                _mockEnderecoService.Object
            );
        }


        [Fact]
        public async Task GetAllClientesAsync_DeveRetornarClientesQuandoExistem()
        {
            var clientes = new List<Cliente> { new Cliente { Id = 1, NomeFantasia = "Empresa 1" } };
            var clientesDto = new List<ClienteDto> { new ClienteDto { Id = 1, NomeFantasia = "Empresa 1" } };

            _mockClienteRepository.Setup(r => r.GetAllClientesAsync()).ReturnsAsync(clientes);
            _mockMapper.Setup(m => m.Map<IEnumerable<ClienteDto>>(clientes)).Returns(clientesDto);

            var result = await _clienteService.GetAllClientesAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetClienteByCnpjAsync_DeveRetornarClienteDtoQuandoEncontrado()
        {
            var cnpj = "12345678000100";
            var cliente = new Cliente { Id = 1, Cnpj = cnpj };
            var clienteDto = new ClienteDto { Id = 1, Cnpj = cnpj };

            _mockClienteRepository.Setup(r => r.GetClienteByCnpjAsync(cnpj)).ReturnsAsync(cliente);
            _mockMapper.Setup(m => m.Map<ClienteDto>(cliente)).Returns(clienteDto);

            var result = await _clienteService.GetClienteByCnpjAsync(cnpj);

            Assert.NotNull(result);
            Assert.Equal(cnpj, result.Cnpj);
        }

        [Fact]
        public async Task GetClienteByIdAsync_DeveRetornarClienteDtoQuandoEncontrado()
        {
            var id = 1;
            var cliente = new Cliente { Id = id };
            var clienteDto = new ClienteDto { Id = id };

            _mockClienteRepository.Setup(r => r.GetByIdClientesAsync(id)).ReturnsAsync(cliente);
            _mockMapper.Setup(m => m.Map<ClienteDto>(cliente)).Returns(clienteDto);

            var result = await _clienteService.GetClienteByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteClienteAsync_DeveRemoverClienteEComplemento()
        {
            var id = 1;
            var cliente = new Cliente
            {
                Id = id,
                EnderecoComplemento = new EnderecoComplemento { Id = 2 }
            };
            var clienteDto = new ClienteDto { Id = id };

            _mockClienteRepository.Setup(r => r.GetByIdClientesAsync(id)).ReturnsAsync(cliente);
            _mockClienteRepository.Setup(r => r.DeleteAsync(cliente)).ReturnsAsync(cliente);
            _mockEnderecoComplementoService
                            .Setup(s => s.DeleteAsync(cliente.EnderecoComplemento.Id))
                            .ReturnsAsync(new EnderecoComplementoDto());
            _mockMapper.Setup(m => m.Map<ClienteDto>(cliente)).Returns(clienteDto);

            var result = await _clienteService.DeleteClienteAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}