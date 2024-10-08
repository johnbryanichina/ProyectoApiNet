﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto_API.Data;
using Proyecto_API.Modelos;
using Proyecto_API.Modelos.Dto;
using Proyecto_API.Repositorio.IRepositorio;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly ILogger<InstrumentsController> _logger;
        private readonly IInstrumentosRepositorio _instrumentosRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public InstrumentsController(ILogger<InstrumentsController> logger, IInstrumentosRepositorio instrumentosRepo, IMapper mapper)
        {
            _logger = logger;
            _instrumentosRepo = instrumentosRepo;
            _mapper = mapper;
            _response = new();
        }
        /*GET INSTRUMENTS*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetInstruments()
        {
            try
            {
                _logger.LogInformation("Obtener Instrumentos");
                IEnumerable<instrumentos> instrumentosList = await _instrumentosRepo.ObtenerTodos();
                //return Ok(InstrumentosStore.instrumentosList);
                _response.Resultado = _mapper.Map<IEnumerable<InstrumentosDto>>(instrumentosList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        /*GET INSTRUMENTS BY ID*/

        [HttpGet("{id:int}", Name = "GetInstrumentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<InstrumentosDto>> GetInstrumentsById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el instrumento con el id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var instrumentos = Ok(InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id));
                var instrumentos = await _instrumentosRepo.Obtener(i => i.id == id);

                if (instrumentos == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<InstrumentosDto>(instrumentos);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                //Envia una lista de errores
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }
        /*POST INSTRUMENTS*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> AgregarInstrumento([FromBody] InstrumentosCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _instrumentosRepo.Obtener(i => i.nombre.ToLower() == createDto.nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("Instrumento con nombre existente", "El instrumento con ese nombre ya existe!");
                    return BadRequest(ModelState);

                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                instrumentos modelo = _mapper.Map<instrumentos>(createDto);

                modelo.fecha_creacion = DateTimeOffset.UtcNow;
                modelo.fecha_actualizacion = DateTimeOffset.UtcNow;
                await _instrumentosRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetInstrumentos", new { id = modelo.id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        /*DELETE INSTRUMENTS*/

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> EliminarInstrumento(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }
                var instrumento = await _instrumentosRepo.Obtener(i => i.id == id);
                if (instrumento == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _instrumentosRepo.Remover(instrumento);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        /*PUT INSTRUMENTS*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarInstrumento(int id, [FromBody] InstrumentosUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            instrumentos modelo = _mapper.Map<instrumentos>(updateDto);

            await _instrumentosRepo.Actualizar(modelo);
            return Ok(_response);
        }

        //HTTP PATCH

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialInstrumento(int id, JsonPatchDocument<InstrumentosUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var instrumento = await _instrumentosRepo.Obtener(i => i.id == id, tracked: false);

            InstrumentosUpdateDto instrumentoDto = _mapper.Map<InstrumentosUpdateDto>(instrumento);
            if (instrumento == null) return BadRequest();

            patchDto.ApplyTo(instrumentoDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            instrumentos modelo = _mapper.Map<instrumentos>(instrumentoDto);

            await _instrumentosRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
