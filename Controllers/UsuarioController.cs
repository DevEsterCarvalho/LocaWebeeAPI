using LocaWebee.Data;
using LocaWebee.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocaWebee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpPost("AdicionarUsuario")]
        public IActionResult AdicionarUsuario([FromBody] Usuario usuario)
        {
            using var context = new AppDbContext();
            var nome = usuario.Nome;
            //Adicionar no banco de dados
            context.Add(usuario);
            context.SaveChanges();
            return Ok("O usuário adicionado foi: " + nome);

        }

        [HttpGet("ObterUsuarios")]
        public IActionResult ObterUsuarios()
        {
            using var context = new AppDbContext();
            var usuarios = context.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpPut("AtualizarUsuarios")]
        public ActionResult AtualizarUsuarios([FromBody] Usuario usuarioAtualizado)
        {
            using var context = new AppDbContext();
            var verificarUsuarioExistente = context.Usuarios.FirstOrDefault(u => u.Id == usuarioAtualizado.Id);

            if (verificarUsuarioExistente == null)
            {
                return NotFound("Usuário não foi encontrado");
            }

            verificarUsuarioExistente.Nome = usuarioAtualizado.Nome;

            context.Update(verificarUsuarioExistente);
            context.SaveChanges();

            return Ok("Usuário foi atualizado");
        }

        [HttpDelete("{Id}")]
        public ActionResult DeletarUsuarios(int Id)
        {
            using var context = new AppDbContext();
            var verificarUsuarioExistente = context.Usuarios.FirstOrDefault(u => u.Id == Id);

            if (verificarUsuarioExistente == null)
            {
                return NotFound("Usuário não foi encontrado");
            }

            context.Usuarios.Remove(verificarUsuarioExistente);
            context.SaveChanges();

            return Ok("Usuário deletado");
        }

       
    }
}
