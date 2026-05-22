using tnsvideos.Data;
using tnsvideos.Models;

namespace tnsvideos.Repository
{
    public class VideosRepository
    {
        private readonly AppDbContext _acciones;

        public VideosRepository(AppDbContext acciones)
        {
            _acciones = acciones;
        }
        public VideoModel crearVideo(VideoModel video)
        {
            _acciones.Videos.Add(video);
            _acciones.SaveChanges();
            return video;
        }
        public List<VideoModel> obtenerVideos()
        {
            return _acciones.Videos?.ToList() ?? new List<VideoModel>();
        }

        public VideoModel? obtenerVideoPorId(int id)
        {
            return _acciones.Videos.Find(id);
        }
        public void eliminarVideo(int id)
        {
            var video = _acciones.Videos.Find(id);
            if (video != null)
            {
                _acciones.Videos.Remove(video);
                _acciones.SaveChanges();
            }
        }
        public bool existeVideo(int id)
        {
            return _acciones.Videos.Any(v => v.Id == id);
        }
        public VideoModel? actualizarVideo(int id, VideoModel videoActualizado)
        {
            var video = _acciones.Videos.Find(id);
            if (video != null)
            {
                video.Titulo = videoActualizado.Titulo;
                video.Descripcion = videoActualizado.Descripcion;
                video.Url = videoActualizado.Url;
                _acciones.SaveChanges();
            }
            return video;
        }
        public VideoModel? obtenerVideoPorTitulo(string titulo)
        {
            return _acciones.Videos
                .FirstOrDefault(v => v.Titulo.ToLower().Contains(titulo.ToLower()));
        }
        public VideoModel? inactivarVideo(int id)
        {
            var video = _acciones.Videos.Find(id);

            if (video == null)
            {
                return null;
            }
            video.Activo = false;
            _acciones.SaveChanges();
            return video; 
        }

    }
}
