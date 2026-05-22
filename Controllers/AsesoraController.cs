using Microsoft.AspNetCore.Mvc;
using tnsvideos.DTO;
using tnsvideos.Models;
using tnsvideos.Services;

namespace tnsvideos.Controllers
{
    [ApiController]
    [Route("api/asesoras")]
    public class AsesoraController : ControllerBase
    {
        private readonly AsesoramarketingService _asesoraService;

        public AsesoraController(AsesoramarketingService asesoraService)
        {
            _asesoraService = asesoraService;
        }

        [HttpPost]
        public ActionResult<AsesoraResponseDto> PostAsesora([FromBody] AsesoraMarketing asesora)
        {
            try
            {
                var creada = _asesoraService.crearAsesora(asesora);
                return StatusCode(201, creada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                var asesora = _asesoraService.Login(dto.NIT, dto.Usuario, dto.Contraseña);
                return Ok(new { success = true, data = asesora });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}