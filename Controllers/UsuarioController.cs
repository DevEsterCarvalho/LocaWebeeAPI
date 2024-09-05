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
    }
}
