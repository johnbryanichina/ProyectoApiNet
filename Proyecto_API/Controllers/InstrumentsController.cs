using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /*GET INSTRUMENTS*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<InstrumentosDto>> GetInstruments()
        {
            return Ok(InstrumentosStore.instrumentosList);
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
                return BadRequest();
            }

            var instrumentos = Ok(InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id));

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
            if (InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Nombre.ToLower() == instrumentosDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Instrumento existente", "El instrumento que usted quiere agregar ya existe!");
                return BadRequest(ModelState);

            }
            if (instrumentosDto == null)
            {
                return BadRequest(instrumentosDto);
            }
            if (instrumentosDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            instrumentosDto.Id = InstrumentosStore.instrumentosList.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
            InstrumentosStore.instrumentosList.Add(instrumentosDto);

            return CreatedAtRoute("GetInstrumentos", new { id = instrumentosDto.Id }, instrumentosDto);
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
            var instrumento = InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id);
            if (instrumento == null)
            {
                return NotFound();
            }
            InstrumentosStore.instrumentosList.Remove(instrumento);

            return NoContent();
        }

        /*PUT INSTRUMENTS*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public IActionResult ActualizarInstrumento(int id, [FromBody] InstrumentosDto instrumentosDto)
        {
            if (instrumentosDto== null || id != instrumentosDto.Id)
            {
                return BadRequest();
            }
            var instrumentos = InstrumentosStore.instrumentosList.FirstOrDefault(i => i.Id == id);
            instrumentos.Nombre = instrumentosDto.Nombre;
            instrumentos.Descripcion = instrumentosDto.Descripcion;

            return NoContent();
        } 
    }
}
