using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        ITecnicoRepository TecnicoRepository { get; }
        IEnderecoComplementoRepository EnderecoComplementoRepository { get; }
        IEnderecoRepository EnderecoRepository { get; }
        IServicosRepository ServicosRepository { get; }

        void Commit();
        Task CommitAsync();
    }
}
