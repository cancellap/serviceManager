using SM.Domaiin.Interfaces;
using SM.Infra.Data;

namespace SM.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IClienteRepository? _clienteRepository;
        private ITecnicoRepository? _tecnicoRepository;
        private IEnderecoComplementoRepository? _enderecoComplementoRepository;
        private IEnderecoRepository? _enderecoRepository;
        private IServicosRepository? _servicosRepository;
        private IUsuarioRepository? _usuarioRepository;
        public AppDbContext _dBContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dBContext = dbContext;
        }


        //lazy loading: adiar a abtenção do objeto até o momento em que ele for realmente necessário
        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                return _usuarioRepository = _usuarioRepository ?? new UsuarioRepository(_dBContext);
            }
        }
        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clienteRepository = _clienteRepository ?? new ClienteRepository(_dBContext);
            }
        }
        public ITecnicoRepository TecnicoRepository
        {
            get
            {
                return _tecnicoRepository = _tecnicoRepository ?? new TecnicoRepository(_dBContext);
            }
        }
        public IEnderecoComplementoRepository EnderecoComplementoRepository
        {
            get
            {
                return _enderecoComplementoRepository = _enderecoComplementoRepository ?? new EnderecoComplementoRepository(_dBContext);
            }
        }
        public IEnderecoRepository EnderecoRepository
        {
            get
            {
                return _enderecoRepository = _enderecoRepository ?? new EnderecoRepository(_dBContext);

            }
        }

        public IServicosRepository ServicosRepository
        {
            get
            {
                return _servicosRepository = _servicosRepository ?? new ServicosRepository(_dBContext);

            }
        }

        public async void Commit()
        {
           _dBContext.SaveChanges();
        }
        public void Dispose()
        {
            _dBContext.Dispose();
        }
        public async Task CommitAsync()
        {
            await _dBContext.SaveChangesAsync();
        }
    }
}
