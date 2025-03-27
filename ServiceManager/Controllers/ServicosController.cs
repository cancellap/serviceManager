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
        [HttpPatch]
        [Route("{idServico}/{idTecnico}")]
        public async Task<ActionResult<ServicosDto>> AddTecnicoAoServico(int idServico, int idTecnico)
        {
            var servicoEditado = await _servicosService.AddTecnicoAoServico(idServico, idTecnico);
            if (servicoEditado == null)
                return null;
            return Ok(servicoEditado);
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
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllServicosAsync()
        {
            var servicosDto = await _servicosService.GetAllServicosAsync();
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteServicoAsync(int id)
        {
            var servicosDto = await _servicosService.DeleteServicoAsync(id);
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
    }
}
