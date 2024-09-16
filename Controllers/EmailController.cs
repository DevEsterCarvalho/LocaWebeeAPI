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
        private static readonly TimeSpan Tempo = TimeSpan.FromHours(1);
        private static readonly int MaxEmails = 50;

        private bool PodeEnviarEmail(int usuarioId, AppDbContext context)
        {
            var agora = DateTime.UtcNow;
            var inicioPeriodo = agora - Tempo;

            var emailsEnviados = context.Emails
                                        .Where(e => e.RemetenteId == usuarioId && e.DataEnvio >= inicioPeriodo)
                                        .Count();

            return emailsEnviados < MaxEmails;
        }


        [HttpPost("EnviarEmail")]
        public IActionResult EnviarEmail([FromBody] EnvioEmail envioEmail)
        {
            using var context = new AppDbContext();

            var remetente = context.Usuarios.FirstOrDefault(u => u.Email == envioEmail.RemetenteEmail);

            if (remetente == null)
            {
                return NotFound("Remetente não encontrado");
            }

            if (!PodeEnviarEmail(remetente.Id, context))
            {
                return BadRequest("Limite de e-mails atingido. Tente novamente mais tarde.");
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
                DataEnvio = DateTime.UtcNow
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
