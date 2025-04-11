using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
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
            var usuario = await _dBContext.Usuarios.Where(user => user.Username == username)
                    .Include(user => user.UsuarioRoles)
                    .ThenInclude(username => username.Role).
                FirstOrDefaultAsync();
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
