using Microsoft.AspNetCore.Mvc;
using tnsvideos.Models;
using tnsvideos.Services;

namespace tnsvideos.Controllers
{
    [ApiController]
    [Route("api/videos")]
    public class VideoController : ControllerBase
    {
        private readonly VideoServices _videoServices;

        public VideoController(VideoServices service)
        {
            _videoServices = service;
        }

        [HttpPost]
        public ActionResult<VideoModel> PostVideo([FromBody] VideoModel video)
        {
            try
            {
                var crearvideo = _videoServices.crearVideo(video);
                return StatusCode(201, crearvideo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<VideoModel>> GetVideos()
        {
            try
            {
                var listavideos = _videoServices.mostrarVideos();
                return Ok(listavideos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideos(int id)
        {
            try
            {
                _videoServices.eliminarVideo(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<VideoModel> UpdateVideo(int id, [FromBody] VideoModel video)
        {
            try
            {
                var videoupdate = _videoServices.actualizarVideo(id, video);
                return Ok(videoupdate);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("buscar")]
        public ActionResult<VideoModel> BuscarVideo([FromQuery] string titulo)
        {
            try
            {
                var video = _videoServices.buscarPorTitulo(titulo);
                return Ok(video);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpPatch("{id}/inactivar")]
        public ActionResult<VideoModel> InactivarVideo(int id)
        {
            try
            {
                var video = _videoServices.inactivarVideo(id);
                return Ok(video);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}