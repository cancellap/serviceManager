using SM.Application.DTOs;
using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IEnderecoComplementoService
    {
        public Task<EnderecoComplementoDto> CreateEnderecoComplementoAsync(EnderecoComplemento enderecoComplemento);

        public Task<EnderecoComplementoDto> DeleteAsync(int id);
    }
}
