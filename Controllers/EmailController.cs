using LocaWebee.Data;
using LocaWebee.Models;
using LocaWebee.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocaWebee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost("EnviarEmail")]
        public IActionResult EnviarEmail([FromBody] EnvioEmail envioEmail)
        {
            using var context = new AppDbContext();

            var remetente = context.Usuarios.FirstOrDefault(u => u.Email == envioEmail.RemetenteEmail);

            if (remetente == null)
            {
                return NotFound("Remetente não encontrado");
            }

            var destinatario = context.Usuarios.FirstOrDefault(u => u.Email == envioEmail.DestinatarioEmail);

            if (destinatario == null)
            {
                return NotFound("Destinatário não encontrado");
            }

            var email = new Email
            {
                RemetenteId = remetente.Id,
                DestinatarioId = destinatario.Id,
                Assunto = envioEmail.Assunto,
                Corpo = envioEmail.Corpo,
            };

            context.Add(email);
            context.SaveChanges();
            return Ok("Email enviado com sucesso!");

        }

        [HttpGet("ObterEmails/{email}")]
        public async Task<IActionResult> ObterEmails(string email)
        {
            var context = new AppDbContext();

            var destinatario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (destinatario == null)
            {
                return NotFound("Destinatário não encontrado");
            }

            var emails = context.Emails.Where(e => e.DestinatarioId == destinatario.Id);

            return Ok(emails);


        }
    }
}
