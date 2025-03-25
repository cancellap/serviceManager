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

        public async Task<Servicos> FindOneId(int id)
        {
            try
            {
                return await _dBContext.Servicos
                    .AsNoTracking()
                    .Include(s => s.Cliente)
                    .Include(s => s.servicoTecnicos)
                        .ThenInclude(st => st.Tecnico)
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEU ERRO NO FIND ONE");
                throw;
            }
        }
    }
}
