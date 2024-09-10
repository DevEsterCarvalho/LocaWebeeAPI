namespace LocaWebee.Requests
{
    public class EnvioEmail
    {
        public string DestinatarioEmail { get; set; }
        public string RemetenteEmail { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
    }
}
