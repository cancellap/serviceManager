using Microsoft.EntityFrameworkCore;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Data;
using SM.Infra.Repositories.Base;
using System.Security.Cryptography.X509Certificates;

namespace SM.Infra.Repositories
{
    public class ServicosRepository : BaseRepository<Servicos>, IServicosRepository
    {
        public ServicosRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Servicos> CreateServicoAsync(Servicos servico)
        {
            servico.CreatedAt = DateTime.UtcNow;
            await _dBContext.Servicos.AddAsync(servico);
            await _dBContext.SaveChangesAsync();
            await _dBContext.SaveChangesAsync();

            return servico;
        }
        public async Task<Servicos> UpdateServicoAsync(Servicos servico)
        {
            _dBContext.Servicos.Update(servico);
            await _dBContext.SaveChangesAsync();
            return servico;
        }

        public async Task<ServicoTecnico> GetByIdsServicoTecnico(int idTecnico)
        {
            var servicoTecnico = await _dBContext.ServicoTecnicos.FirstOrDefaultAsync(st => st.TecnicoId == idTecnico);
            return servicoTecnico;
        }

        public async Task<ServicoTecnico> CreateServicoTecnicoAsync(ServicoTecnico servicoTecnico)
        {
            await _dBContext.ServicoTecnicos.AddAsync(servicoTecnico);
            await _dBContext.SaveChangesAsync();
            return servicoTecnico;
        }

        public async Task<Servicos> FindOneId(int id)
        {

            return await _dBContext.Servicos
                .AsNoTracking()
                .Include(s => s.Cliente)
                .Include(s => s.servicoTecnicos)
                    .ThenInclude(st => st.Tecnico)
                .FirstOrDefaultAsync(s => s.Id == id);

        }
        public async Task<List<Servicos>> GetAllAsync()
        {
            return await _dBContext.Servicos
                .AsNoTracking()
                .Include(s => s.Cliente)
                .Include(s => s.servicoTecnicos)
                    .ThenInclude(st => st.Tecnico)
                .ToListAsync();
        }

        public async Task<Servicos> DeleteServicoAsync(int id)
        {
            var servico = await _dBContext.Servicos
                .Include(s => s.servicoTecnicos)
                .FirstOrDefaultAsync(s => s.Id == id);

            var servicosTecnicos = await _dBContext.ServicoTecnicos
                .Where(st => st.ServicoId == id)
                .ToListAsync();

            if (servico == null)
                return null;

            servico.IsAtivo = false;
            servico.DeletedAt = DateTime.UtcNow;
            servico.IsDeleted = true;
            await _dBContext.SaveChangesAsync();
            return servico;
        }

        public async Task<List<Servicos>> GetServicosWithFilterAsync(ServicoFiltro filtro)
        {
            var servicos = await _dBContext.Servicos
                .Where(s => s.ClienteId.Equals(filtro.ClienteId))  
                .Where(s => s.servicoTecnicos.Any(st => filtro.TecnicosIds.Contains(st.TecnicoId))) 
                .Include(s => s.servicoTecnicos)  
                    .ThenInclude(st => st.Tecnico)
                .ToListAsync();

            return servicos;
        }

    }
}
