using SM.Application.DTOs;
using SM.Domaiin.Entities;
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
        Task<List<ServicosDto>> GetAllServicosAsync();
        Task<ServicosDto> GetServicoByIdAsync(int id);
        Task<ServicosDto> DeleteServicoAsync(int id);
        Task<ServicosDto> AddTecnicoAoServico(int idServico, int idTecnico);
        Task<List<ServicosDto>> GetServicosWithFilterAsync(ServicoFiltro filtro);
    }
}
