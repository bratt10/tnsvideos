namespace tnsvideos.Models
{
    public class AsesoraMarketing
    {
        public int Id { get; set; }  
        public string Nombre { get; set; }
        public string mail { get; set; }
        public int NIT { get; set; }
        public string Usuario { get; set; } = "";

        public string Contraseña { get; set; } = "";

    }
}
