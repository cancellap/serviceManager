﻿using SM.Domaiin.Entities.Base;

namespace SM.Domaiin.Entities
{
    public class Servicos : BaseEntity
    {
        public string Descricao { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List<ServicoTecnico> servicoTecnicos { get; set; }
        public bool IsAtivo { get; set; }

    }
}
