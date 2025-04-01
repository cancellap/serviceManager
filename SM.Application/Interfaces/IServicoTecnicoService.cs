using SM.Application.DTOs;
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IServicoTecnicoService
    {
        public Task<ServicoTecnicoDto> CreateServicoTecnicoAsync(Servicos servicos);
        public Task<ServicoTecnicoDto> UpdateServicoTecnicoAsync(Servicos servicos);
        public Task<ServicoTecnicoDto> GetServicoTecnicoByIdAsync(int idServico, int idTecnico);
    }
}
