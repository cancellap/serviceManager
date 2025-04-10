using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;

namespace ServiceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> AddUser(UsuarioCreateDto usuarioCreateDto)
        {
            try
            {
                var usuario = await _usuarioService.AddUser(usuarioCreateDto);
                return usuario;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }

        }
        [HttpGet("username/{username}")]
        public async Task<ActionResult<UsuarioDto>> GetByUsername(string username)
        {
            try
            {
                var usuario = await _usuarioService.GetByUsername(username);
                return usuario;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar usuário: {ex.Message}");
            }
        }
    }
}
