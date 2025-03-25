using Microsoft.AspNetCore.Mvc;
using SM.Application.DTOs;
using SM.Application.Service;
using SM.Domaiin.Entities;

namespace ServiceManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly ServicosService _servicosService;
        public ServicosController(ServicosService servicosService)
        {
            _servicosService = servicosService;
        }

        [HttpPost]
        [Route("{idCliente}")]
        public async Task<ActionResult<ServicosDto>> CreateServicosAsync([FromBody] ServicosCreateDto servicosCreateDto, int idCliente)
        {
            if (servicosCreateDto == null)
            {
                return BadRequest("Os dados do serviço não podem ser nulos.");
            }
            try
            {
                var servicosDto = await _servicosService.CreateServicoAsync(servicosCreateDto, idCliente);
                var uri = Url.Action(nameof(GetServicosByIdAsync), new { id = servicosDto.Id });
                return Created(uri, servicosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar serviço: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetServicosByIdAsync(int id)
        {
            var servicosDto = await _servicosService.GetServicoByIdAsync(id);
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
    }
}
