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
        public ActionResult <VideoModel> PostVideo([FromBody] VideoModel video)
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
    }
}