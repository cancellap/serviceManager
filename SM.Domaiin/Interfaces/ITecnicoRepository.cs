using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface ITecnicoRepository
    {
        public Task<Tecnico> AddAsync(Tecnico tecnico);
        public Task<Tecnico> FindOneId(int id);
        public Task<Tecnico> updateTecnicoAsync(Tecnico tecnico);
        public Task<List<Tecnico>> GetAllTecnicosAsync();
        public Task<Tecnico> GetTecnicoByIdAsync(int id);
        public Task<Tecnico> GetTecnicoByCpfAsync(string cpf);
        public Task<Tecnico> DeleteAsync(Tecnico tecnico);
    }
}
