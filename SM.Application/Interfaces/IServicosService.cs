using SM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
     public interface IServicosService
    {
        Task<ServicosDto> CreateServicoAsync(ServicosCreateDto servicosCreateDto, int idCliente);
        Task<IEnumerable<ServicosDto>> GetAllServicosAsync();
        Task<ServicosDto> GetServicoByIdAsync(int id);
        Task<ServicosDto> DeleteServicoAsync(int id);
    }
}
