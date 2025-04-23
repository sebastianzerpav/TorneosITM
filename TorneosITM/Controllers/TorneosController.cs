using Microsoft.AspNetCore.Mvc;
using TorneosITM.Data.Models;
using TorneosITM.Services;

namespace TorneosITM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneosController : ControllerBase
    {
        //CRUD Torneos
        private readonly ITorneoService torneoService;
        public TorneosController(ITorneoService torneoService) { 
            this.torneoService = torneoService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Torneo torneo)
        {
            bool respuesta = await torneoService.Insert(torneo);
            if (respuesta) {
                return Ok("Torneo insertado exitosamente");
            }else {
                return StatusCode(500, "No pudo insertarse el torneo. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] Torneo torneo)
        {
            bool respuesta = await torneoService.Update(id, torneo);
            if (respuesta)
            {
                return Ok("Torneo actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el torneo. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            bool respuesta = await torneoService.Delete(id);
            if (respuesta)
            {
                return Ok("Torneo eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el torneo. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpGet("GetByType")]
        public async Task<IActionResult> GetByType([FromQuery] string type)
        {
            IEnumerable<Torneo> torneos = await torneoService.GetByType(type);
            if (!torneos.Any()) { return NotFound("No hay ningún torneo de ese tipo."); }
            else
            {
                return Ok(torneos);
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            IEnumerable<Torneo> torneos = await torneoService.GetByName(name);
            if (!torneos.Any()) { return NotFound("No hay ningún torneo con el nombre."); }
            else
            {
                return Ok(torneos);
            }
        }

        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate([FromQuery] DateOnly date)
        {
            IEnumerable<Torneo> torneos = await torneoService.GetByDate(date);
            if (!torneos.Any()) { return NotFound("No hay ningún torneo para esa fecha."); }
            else
            {
                return Ok(torneos);
            }
        }
    }
}
