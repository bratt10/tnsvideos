using tnsvideos.Models;
using tnsvideos.Repository;

namespace tnsvideos.Services
{
    public class VideoServices
    {
        private readonly VideosRepository _videorepository;

        public VideoServices(VideosRepository repository)
        {
            _videorepository = repository;
        }

        public VideoModel crearVideo(VideoModel videoingreso)
        {
            if (string.IsNullOrWhiteSpace(videoingreso.Titulo))
            {
                throw new Exception("El titulo debe ser obligatorio");
            }
            if (string.IsNullOrWhiteSpace(videoingreso.Url))
            {
                throw new Exception("La URL no puede estar vacia");
            }

            return _videorepository.crearVideo(videoingreso);
        }
        public List<VideoModel> mostrarVideos()
        {
            var videos = _videorepository.obtenerVideos();
            if (!videos.Any())
            {
                throw new Exception("No existen videos");
            }
            return videos;

        }
        public void eliminarVideo(int id)
        {
            if (!_videorepository.existeVideo(id))
            {
                throw new Exception("El video no existe");
            }
            _videorepository.eliminarVideo(id);
        }
        public VideoModel? actualizarVideo(int id, VideoModel videoActualizado)
        {
            if (!_videorepository.existeVideo(id))
            {
                throw new Exception("El video no existe");
            }
            if (string.IsNullOrWhiteSpace(videoActualizado.Titulo))
            {
                throw new Exception("El titulo debe ser obligatorio");
            }
            if (string.IsNullOrWhiteSpace(videoActualizado.Url))
            {
                throw new Exception("La URL no puede estar vacia");
            }
            return _videorepository.actualizarVideo(id, videoActualizado);
        }
    }
}
        

