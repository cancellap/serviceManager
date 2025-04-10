using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Data;
using SM.Infra.Repositories.Base;

namespace SM.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext dBContext) : base(dBContext)
        {

        }
        public async Task<Usuario?> GetByUsername(string username)
        {
            var usuario = _dBContext.Usuarios.FirstOrDefault(u => u.Username == username);
            return usuario;
        }
        public async Task<Usuario> AddAsync(Usuario entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dBContext.Set<Usuario>().AddAsync(entity);
            return entity;
        }

    }


}
