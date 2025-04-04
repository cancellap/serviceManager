
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface IServicosRepository
    {
        public Task<Servicos> CreateServicoAsync(Servicos servico);
        public Task<Servicos> FindOneId(int id);
        public Task<ServicoTecnico> CreateServicoTecnicoAsync(ServicoTecnico servicoTecnico);
        public Task<List<Servicos>> GetAllAsync();
        public Task<Servicos> UpdateServicoAsync(Servicos servico);
        public Task<ServicoTecnico> GetByIdsServicoTecnico(int idTecnico);
        public Task<Servicos> DeleteServicoAsync(int id);
        public Task<List<Servicos>> GetServicosWithFilterAsync(ServicoFiltro filtro);
        Task<List<ServicoTecnico>> GetAllByServicoIdAsync(int servicoId);
        void DeleteServicoTecnicos(List<ServicoTecnico> servicoTecnicos);

    }
}
