using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<Produto>> Listar()
    {
        return await _produtoRepository.ListarTodosAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<Produto> SelecionarPorId(int id)
    {
        return await _produtoRepository.BuscarPorIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Adicionar(Produto produto)
    {
        await _produtoRepository.AdicionarAsync(produto);
        return Ok(produto);
        //return CreatedAtAction(nameof(produto), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();

        await _produtoRepository.AtualizarAsync(produto);
        return Ok(produto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _produtoRepository.DeletarAsync(id);
        return Ok();
    }
}
