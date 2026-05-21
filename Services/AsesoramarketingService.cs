using tnsvideos.DTO;
using tnsvideos.Models;
using tnsvideos.Repository;
using BCrypt.Net;


namespace tnsvideos.Services
{
    public class AsesoramarketingService
    {
        private readonly AsesoraMarkentingRepository asesorarepository;

        public AsesoramarketingService(AsesoraMarkentingRepository repository)
        {
            asesorarepository = repository;
        }

        public AsesoraResponseDto crearAsesora(AsesoraMarketing asesoraingreso)
        {
            asesoraingreso.Contraseña = BCrypt.Net.BCrypt.HashPassword(asesoraingreso.Contraseña);
            if (string.IsNullOrWhiteSpace(asesoraingreso.Nombre))
            {
                throw new Exception("El nombre debe ser obligatorio");
            }
            if (asesoraingreso.NIT <= 0)
            {
                throw new Exception("El NIT debe ser un numero positivo");
            }
            if (string.IsNullOrWhiteSpace(asesoraingreso.Usuario))
            {
                throw new Exception("El usuario no puede estar vacio");
            }
            var guardada = asesorarepository.crearAsesora(asesoraingreso);
            return new AsesoraResponseDto
            {
                Id = guardada.Id,
                Nombre = guardada.Nombre,
                Mail = guardada.mail,
                NIT = guardada.NIT,
                Usuario = guardada.Usuario
            };

        }
        public AsesoraResponseDto Login(int NIT, string usuario, string contraseña)
        {
            var asesora = asesorarepository.BuscarPorUsuario(NIT);

            if (asesora == null)
                throw new Exception("Usuario no encontrado");

            bool esValida = BCrypt.Net.BCrypt.Verify(contraseña, asesora.Contraseña);

            if (!esValida)
                throw new Exception("Contraseña incorrecta");

            return new AsesoraResponseDto
            {
                Id = asesora.Id,
                Nombre = asesora.Nombre,
                Mail = asesora.mail,
                NIT = asesora.NIT,
                Usuario = asesora.Usuario
            };
        }
    }
}