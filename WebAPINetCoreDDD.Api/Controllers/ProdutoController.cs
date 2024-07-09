using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;
using WebAPINetCoreDDD.Persistence.Repositories;

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
    public async Task<IActionResult> Adicionar(Produto Produto)
    {
        await _produtoRepository.AdicionarAsync(Produto);
        return CreatedAtAction(nameof(Produto), new { id = Produto.Id }, Produto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(int id, Produto Produto)
    {
        if (id != Produto.Id) return BadRequest();

        await _produtoRepository.AtualizarAsync(Produto);
        return Ok(Produto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _produtoRepository.DeletarAsync(id);
        return Ok();
    }
}
