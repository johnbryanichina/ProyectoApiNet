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
        public ActionResult<IEnumerable<InstrumentosDto>> GetInstruments()
        {
            _logger.LogInformation("Obtener Instrumentos");
            //return Ok(InstrumentosStore.instrumentosList);
            return Ok(_db.instrumentos.ToList());
        }

        /*GET INSTRUMENTS BY ID*/

        [HttpGet("{id:int}", Name = "GetInstrumentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<InstrumentosDto> GetInstrumentsById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al obtener el instrumento con el id "+id);
                return BadRequest();
            }

            //var instrumentos = Ok(InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id));
            var instrumentos = Ok(_db.instrumentos.FirstOrDefault(i => i.id == id));

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

        public ActionResult<InstrumentosDto> AgregarInstrumento([FromBody] InstrumentosDto instrumentosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.instrumentos.FirstOrDefault(i => i.nombre.ToLower() == instrumentosDto.nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Instrumento existente", "El instrumento que usted quiere agregar ya existe!");
                return BadRequest(ModelState);

            }
            if (instrumentosDto == null)
            {
                return BadRequest(instrumentosDto);
            }
            if (instrumentosDto.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            instrumentos modelo = new()
            {
                nombre = instrumentosDto.nombre,
                descripcion = instrumentosDto.descripcion,
                precio = instrumentosDto.precio,
                cantidad = instrumentosDto.cantidad,
                imagenUrl = instrumentosDto.imagenUrl,
            };
            _db.instrumentos.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetInstrumentos", new { id = instrumentosDto.id }, instrumentosDto);
        }
        /*DELETE INSTRUMENTS*/

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult EliminarInstrumento(int id) {
            if (id == 0)
            {
                return BadRequest();

            }
            var instrumento = _db.instrumentos.FirstOrDefault(i => i.id == id);
            if (instrumento == null)
            {
                return NotFound();
            }
            _db.instrumentos.Remove(instrumento);
            _db.SaveChanges();

            return NoContent();
        }

        /*PUT INSTRUMENTS*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public IActionResult ActualizarInstrumento(int id, [FromBody] InstrumentosDto instrumentosDto)
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
            _db.SaveChanges();
            return NoContent();
        }

        //HTTP PATCH

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialInstrumento(int id, JsonPatchDocument<InstrumentosDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var instrumento = _db.instrumentos.AsNoTracking().FirstOrDefault(i => i.id == id);
            InstrumentosDto instrumentoDto = new()
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
            _db.SaveChanges();
            return NoContent();
        }
    }
}
