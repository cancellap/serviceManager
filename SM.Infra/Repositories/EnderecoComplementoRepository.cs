using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Data;
using SM.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infra.Repositories
{
    public class EnderecoComplementoRepository : BaseRepository<EnderecoComplemento>, IEnderecoComplementoRepository
    {
        public EnderecoComplementoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<EnderecoComplemento> AddAsync(EnderecoComplemento enderecoComplemento)
        {
            await _dBContext.EnderecoComplementos.AddAsync(enderecoComplemento);
            await _dBContext.SaveChangesAsync();
            return enderecoComplemento;
        }

        public async Task<EnderecoComplemento> DeleteAsync(EnderecoComplemento enderecoComplemento)
        {
            enderecoComplemento.DeletedAt = DateTime.UtcNow;
            enderecoComplemento.IsDeleted = true;
            await _dBContext.SaveChangesAsync();
            return enderecoComplemento;
        }

        public async Task<EnderecoComplemento> GetByIdEnderecoComplementoAsync(int id)
        {
            return await _dBContext.EnderecoComplementos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EnderecoComplemento>  UpdateEnderecoComplementoAsync(EnderecoComplemento enderecoComplemento)
        {
            _dBContext.EnderecoComplementos.Update(enderecoComplemento);
            await _dBContext.SaveChangesAsync();
            return enderecoComplemento;
        }
    }
}
