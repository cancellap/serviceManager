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


            if (servico.servicoTecnicos != null && servico.servicoTecnicos.Any())
            {
                foreach (var servicoTecnico in servico.servicoTecnicos)
                {
                    var tecnicoExistente = await _dBContext.ServicoTecnicos
                                                   .FirstOrDefaultAsync(st => st.TecnicoId == servicoTecnico.TecnicoId
                                                                                && st.ServicoId != servico.Id);

                    if (tecnicoExistente != null)
                    {
                        throw new InvalidOperationException($"O técnico com ID {servicoTecnico.TecnicoId} já está associado a outro serviço.");
                    }

                    servicoTecnico.ServicoId = servico.Id;

                    await _dBContext.ServicoTecnicos.AddAsync(servicoTecnico);
                }

            }
            await _dBContext.Servicos.AddAsync(servico);
            await _dBContext.SaveChangesAsync();
            await _dBContext.SaveChangesAsync();

            return servico;
        }
        public async Task<Servicos> UpdateServicosAsync(Servicos servico)
        {
            _dBContext.Servicos.Update(servico);
            await _dBContext.SaveChangesAsync();
            return servico;
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
        public async Task<IEnumerable<Servicos>> GetAllAsync()
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
    }
}
