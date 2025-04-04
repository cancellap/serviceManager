﻿using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco?> GetEnderecoByDetailsAsync(string rua, string cidade, string estado, string cep);

        Task<Endereco> AddAsync(Endereco endereco);
    }
}
