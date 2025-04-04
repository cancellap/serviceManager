﻿using Microsoft.AspNetCore.Mvc;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Application.Service;
using SM.Domaiin.Entities;

namespace ServiceManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly IServicosService _servicosService;
        public ServicosController(IServicosService servicosService)
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
        [Route("getServicosWithFilter")]
        public async Task<ActionResult<List<ServicosDto>>> GetServicosWithFilterAsync([FromBody] ServicoFiltro filtro)
        {
            var servicosDto = await _servicosService.GetServicosWithFilterAsync(filtro);
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
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
        public async Task<ActionResult<Servicos>> GetServicosByIdAsync(int id)
        {
            var servicosDto = await _servicosService.GetServicoByIdAsync(id);
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<Servicos>>> GetAllServicosAsync()
        {
            var servicosDto = await _servicosService.GetAllServicosAsync();
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Servicos>> DeleteServicoAsync(int id)
        {
            var servicosDto = await _servicosService.DeleteServicoAsync(id);
            if (servicosDto == null)
                return NotFound();
            return Ok(servicosDto);
        }
    }
}
