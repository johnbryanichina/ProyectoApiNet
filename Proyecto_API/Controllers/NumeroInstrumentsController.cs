using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public class NumeroInstrumentsController : ControllerBase
    {
        private readonly ILogger<NumeroInstrumentsController> _logger;
        private readonly IInstrumentosRepositorio _instrumentosRepo;
        private readonly INumeroInstrumentosRepositorio _numeroRepo;

        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroInstrumentsController(ILogger<NumeroInstrumentsController> logger, IInstrumentosRepositorio instrumentosRepo, INumeroInstrumentosRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _instrumentosRepo = instrumentosRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroInstrumentos()
        {
            try
            {
                _logger.LogInformation("Obtener número Instrumentos");
                IEnumerable<numero_instrumentos> numeroinstrumentosList = await _numeroRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroInstrumentoDto>>(numeroinstrumentosList);
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

        [HttpGet("{id:int}", Name = "GetNumeroInstrumentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<NumeroInstrumentoDto>> GetNumeroInstrumentsById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el numero de instrumento con el id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var instrumentos = Ok(InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id));
                var numeroinstrumentos = await _numeroRepo.Obtener(i => i.instrumento_no == id);

                if (numeroinstrumentos == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroInstrumentoDto>(numeroinstrumentos);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CrearNumeroInstrumento([FromBody] NumeroInstrumentoDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numeroRepo.Obtener(i => i.instrumento_no == createDto.instrumento_no) != null)
                {
                    ModelState.AddModelError("Instrumento existente", "El numero de instrumento que usted quiere agregar ya existe!");
                    return BadRequest(ModelState);

                }
                if (await _instrumentosRepo.Obtener(i => i.id == createDto.instrumento_id) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El ID de ese instrumento no existe!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                numero_instrumentos modelo = _mapper.Map<numero_instrumentos>(createDto);

                modelo.fecha_creacion = DateTimeOffset.UtcNow;
                modelo.fecha_actualizacion = DateTimeOffset.UtcNow;
                await _numeroRepo.Crear(modelo);

                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetInstrumentos", new { id = modelo.instrumento_no }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> EliminarNumeroInstrumento(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }
                var numeroinstrumento = await _numeroRepo.Obtener(i => i.instrumento_no == id);
                if (numeroinstrumento == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroRepo.Remover(numeroinstrumento);
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


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarNumeroInstrumento(int id, [FromBody] NumeroInstrumentoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.instrumento_no)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (await _instrumentosRepo.Obtener(i => i.id == updateDto.instrumento_id) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id del instrumento No existe!");
                return BadRequest(ModelState);
            }


            numero_instrumentos modelo = _mapper.Map<numero_instrumentos>(updateDto);

            await _numeroRepo.Actualizar(modelo);
            return Ok(_response);
        }
    }
}
