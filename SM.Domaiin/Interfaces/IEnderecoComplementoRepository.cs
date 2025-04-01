using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface IEnderecoComplementoRepository
    {
        public Task<EnderecoComplemento> AddAsync(EnderecoComplemento enderecoComplemento);
        public Task<EnderecoComplemento> GetByIdEnderecoComplementoAsync(int id);
        public Task<EnderecoComplemento> UpdateEnderecoComplementoAsync(EnderecoComplemento enderecoComplemento);
        public Task<EnderecoComplemento> DeleteAsync(EnderecoComplemento enderecoComplemento);
    }
}
