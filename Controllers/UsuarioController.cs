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
            var nome = usuario.Nome;
            return Ok("O usuário adicionado foi: " + nome);
        }
    }
}
