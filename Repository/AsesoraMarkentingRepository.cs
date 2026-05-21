using tnsvideos.Data;
using tnsvideos.Models;

namespace tnsvideos.Repository
{
    public class AsesoraMarkentingRepository  
    {
        private readonly AppDbContext _CreacionAsesora;

        public AsesoraMarkentingRepository(AppDbContext CreacionAsesora)
        {
            _CreacionAsesora = CreacionAsesora;
        }
        public AsesoraMarketing crearAsesora(AsesoraMarketing asesora)
        {
            _CreacionAsesora.Asesoras.Add(asesora);
            _CreacionAsesora.SaveChanges();
            return asesora;
        }
        public AsesoraMarketing? BuscarPorUsuario(int NIT)
        {
            return _CreacionAsesora.Asesoras.FirstOrDefault(a => a.NIT == NIT);
        }
    }
}