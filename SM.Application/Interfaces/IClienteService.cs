using SM.Application.DTOs;

namespace SM.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> CreateClienteAsync(ClienteCreateDto clienteDto);
        Task<IEnumerable<ClienteDto>> GetAllClientesAsync();
        Task<ClienteDto> GetClienteByIdAsync(int id);
        Task<ClienteDto> DeleteClienteAsync(int id);
        Task<ClienteDto> GetClienteByCnpjAsync(string cnpj);

    }
}
