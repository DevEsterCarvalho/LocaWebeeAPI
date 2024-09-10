using System.ComponentModel.DataAnnotations;

namespace LocaWebee.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public int RemetenteId { get; set; }
        public int DestinatarioId { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }

        public virtual Usuario Remetente { get; set; }
        public virtual Usuario Destinatario { get; set; }
    }
}
