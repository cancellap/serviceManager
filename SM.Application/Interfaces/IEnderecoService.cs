﻿using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<int> ObterOuCriarEnderecoAsync(Endereco endereco);
    }
}
