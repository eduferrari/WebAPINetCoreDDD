using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IEnumerable<Usuario>> GetUsers()
    {
        return await _usuarioRepository.ListarTodosAsync();
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Usuario> GetUser(int id)
    {
        return await _usuarioRepository.BuscarPorIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUser(Usuario usuario)
    {
        await _usuarioRepository.AdicionarAsync(usuario);
        return Ok(usuario);
        //return CreatedAtAction(nameof(GetUser), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, Usuario usuario)
    {
        if (id != usuario.Id) return BadRequest();

        await _usuarioRepository.AtualizarAsync(usuario);
        return Ok(usuario);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _usuarioRepository.DeletarAsync(id);
        return Ok();
    }
}