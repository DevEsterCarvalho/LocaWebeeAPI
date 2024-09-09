using LocaWebee.Enums;
using System.ComponentModel.DataAnnotations;

namespace LocaWebee.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public EnumTema Tema { get; set; }
    }
}
