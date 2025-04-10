using AutoMapper;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;

namespace SM.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<UsuarioDto> AddUser(UsuarioCreateDto usuarioCreateDto)
        {
            if (usuarioCreateDto.Password != usuarioCreateDto.ConfirmPassword)
                throw new InvalidOperationException("As senhas não conferem.");

            var usuarioExistente = await _unitOfWork.UsuarioRepository.GetByUsername(usuarioCreateDto.Username);
            if (usuarioExistente != null)
                throw new InvalidOperationException("Usuário já existe.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuarioCreateDto.Password);

            var usuario = new Usuario
            {
                Username = usuarioCreateDto.Username,
                Password = hashedPassword,
                UsuarioRoles = new List<UsuarioRole>()
            };

            foreach (var roleId in usuarioCreateDto.RoleIds.Distinct())
            {
                usuario.UsuarioRoles.Add(new UsuarioRole
                {
                    RoleId = roleId
                });
            }

            var usuarioCriado = await _unitOfWork.UsuarioRepository.AddAsync(usuario);
            await _unitOfWork.CommitAsync();

            var dto = _mapper.Map<UsuarioDto>(usuarioCriado);
            return dto;
        }


        public Task<Usuario> EditRolesOfUser(string username, List<string> roles)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> GetByUsername(string username)
        {
            var usuario =  await _unitOfWork.UsuarioRepository.GetByUsername(username);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");
            return _mapper.Map<UsuarioDto>(usuario);

        }

        public Task RemoveUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
