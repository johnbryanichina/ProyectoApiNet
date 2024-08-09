using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API.Data;
using Proyecto_API.Modelos;
using Proyecto_API.Modelos.Dto;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly ILogger<InstrumentsController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public InstrumentsController(ILogger<InstrumentsController> logger, ApplicationDbContext db, IMapper mapper)
        {
           _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        /*GET INSTRUMENTS*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InstrumentosDto>>> GetInstruments()
        {
            _logger.LogInformation("Obtener Instrumentos");
            IEnumerable<instrumentos> instrumentosList = await _db.instrumentos.ToListAsync();
            //return Ok(InstrumentosStore.instrumentosList);
            return Ok(_mapper.Map<IEnumerable<InstrumentosDto>>(instrumentosList));
        }

        /*GET INSTRUMENTS BY ID*/

        [HttpGet("{id:int}", Name = "GetInstrumentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<InstrumentosDto>> GetInstrumentsById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al obtener el instrumento con el id "+id);
                return BadRequest();
            }

            //var instrumentos = Ok(InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id));
            var instrumentos = await _db.instrumentos.FirstOrDefaultAsync(i => i.id == id);

            if (instrumentos == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<InstrumentosDto>(instrumentos));
        }
        /*POST INSTRUMENTS*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<InstrumentosDto>> AgregarInstrumento([FromBody] InstrumentosCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ( await _db.instrumentos.FirstOrDefaultAsync(i => i.nombre.ToLower() == createDto.nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Instrumento existente", "El instrumento que usted quiere agregar ya existe!");
                return BadRequest(ModelState);

            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
           
            instrumentos modelo = _mapper.Map<instrumentos>(createDto);
             
            await _db.instrumentos.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetInstrumentos", new { id = modelo.id }, modelo);
        }
        /*DELETE INSTRUMENTS*/

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> EliminarInstrumento(int id){
            if (id == 0)
            {
                return BadRequest();

            }

            var instrumento = await _db.instrumentos.FirstOrDefaultAsync(i => i.id == id);

            if (instrumento == null)
            {
                return NotFound();
            }

            _db.instrumentos.Remove(instrumento);
           await _db.SaveChangesAsync();

            return NoContent();
        }

        /*PUT INSTRUMENTS*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarInstrumento(int id, [FromBody] InstrumentosUpdateDto updateDto)
        {
            if (updateDto== null || id != updateDto.id)
            {
                return BadRequest();
            }
         

            instrumentos modelo = _mapper.Map<instrumentos>(updateDto);

            _db.instrumentos.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
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
            var instrumento = await _db.instrumentos.AsNoTracking().FirstOrDefaultAsync(i => i.id == id);

            InstrumentosUpdateDto instrumentoDto = _mapper.Map<InstrumentosUpdateDto>(instrumento);

             
            if (instrumento == null) return BadRequest();

            patchDto.ApplyTo(instrumentoDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            instrumentos modelo = _mapper.Map<instrumentos>(instrumentoDto);
             
            _db.instrumentos.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
