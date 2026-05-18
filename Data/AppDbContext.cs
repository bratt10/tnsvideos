using Microsoft.EntityFrameworkCore;
using tnsvideos.Models;

namespace tnsvideos.Data

{
    public class AppDbContext : DbContext //se herda de DbContext para poder usarlo como contexto de base de datos
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // DbSet representa una tabla en la base de datos para generar el CRUD de la entidad VideoModel basicamente crea la tabla en la base de datos y permite realizar operaciones sobre ella
        public DbSet<VideoModel> Videos { get; set; }

        //si hubiesen varias tablas se agregarian mas DbSet para cada una de las entidades que se quieran manejar en la base de datos
    }
}
