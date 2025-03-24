﻿using SM.Domaiin.Entities.Base;
using SM.Domaiin.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Entities
{
    public class Cliente : BaseEntity
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        [InverseProperty("Cliente")]
        public EnderecoComplemento? EnderecoComplemento { get; set; }
        public ICollection<Servicos> Servicos { get; set; }

        public Cliente() { }

        public Cliente(Cliente c)
        {
            ValidateDomain(c);
            NomeFantasia = c.NomeFantasia;
            RazaoSocial = c.RazaoSocial;
            Email = c.Email;
            Cnpj = c.Cnpj;
            EnderecoComplemento = c.EnderecoComplemento;

        }
        private static void ValidateDomain(Cliente c)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(c.NomeFantasia), "Nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(c.Email), "Email é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(c.Cnpj), "Cnpj é obrigatório");
            DomainExceptionValidation.When(!ValidaCnpj.IsCnpj(c.Cnpj), "Cnpj inválido");
            DomainExceptionValidation.When(c.EnderecoComplemento == null, "Endereço é obrigatório");
        }
    }
}
