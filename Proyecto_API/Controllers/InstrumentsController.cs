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
        public InstrumentsController(ILogger<InstrumentsController> logger, ApplicationDbContext db)
        {
           _logger = logger;
            _db = db;
        }
        /*GET INSTRUMENTS*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InstrumentosDto>>> GetInstruments()
        {
            _logger.LogInformation("Obtener Instrumentos");
            //return Ok(InstrumentosStore.instrumentosList);
            return Ok(await _db.instrumentos.ToListAsync());
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

            return Ok(instrumentos);
        }
        /*POST INSTRUMENTS*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<InstrumentosDto>> AgregarInstrumento([FromBody] InstrumentosCreateDto instrumentosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ( await _db.instrumentos.FirstOrDefaultAsync(i => i.nombre.ToLower() == instrumentosDto.nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Instrumento existente", "El instrumento que usted quiere agregar ya existe!");
                return BadRequest(ModelState);

            }
            if (instrumentosDto == null)
            {
                return BadRequest(instrumentosDto);
            }
           
            instrumentos modelo = new()
            {
                nombre = instrumentosDto.nombre,
                descripcion = instrumentosDto.descripcion,
                precio = instrumentosDto.precio,
                cantidad = instrumentosDto.cantidad,
                imagenUrl = instrumentosDto.imagenUrl,
            };
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
        public async Task<IActionResult> ActualizarInstrumento(int id, [FromBody] InstrumentosUpdateDto instrumentosDto)
        {
            if (instrumentosDto== null || id != instrumentosDto.id)
            {
                return BadRequest();
            }
            /*var instrumentos = InstrumentosStore.instrumentosList.FirstOrDefault(i => i.id == id);
            instrumentos.nombre = instrumentosDto.nombre;
           instrumentos.descripcion = instrumentosDto.descripcion;
           instrumentos.nombre = instrumentosDto.nombre;
           instrumentos.cantidad = instrumentosDto.cantidad;
           instrumentos.imagenUrl = instrumentosDto.imagenUrl;*/

            instrumentos modelo = new()
            {
                id = instrumentosDto.id,
                nombre = instrumentosDto.nombre,
                descripcion = instrumentosDto.descripcion,
                cantidad = instrumentosDto.cantidad,
                imagenUrl = instrumentosDto.imagenUrl,
            };
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
            InstrumentosUpdateDto instrumentoDto = new()
            {
                id = instrumento.id,
                nombre = instrumento.nombre,
                descripcion = instrumento.descripcion,
                cantidad = instrumento.cantidad,
                precio = instrumento.precio,
            };
            if (instrumento == null) return BadRequest();

            patchDto.ApplyTo(instrumentoDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            instrumentos modelo = new()
            {
                id = instrumentoDto.id,
                nombre = instrumentoDto.nombre,
                descripcion = instrumentoDto.descripcion,
                cantidad = instrumentoDto.cantidad,
                precio = instrumentoDto.precio,
            };
            _db.instrumentos.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
