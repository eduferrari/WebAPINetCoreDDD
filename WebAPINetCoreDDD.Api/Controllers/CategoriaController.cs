using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<Categoria>> Listar()
    {
        return await _categoriaRepository.ListarTodosAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<Categoria> SelecionarPorId(int id)
    {
        return await _categoriaRepository.BuscarPorIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Adicionar(Categoria categoria)
    {
        await _categoriaRepository.AdicionarAsync(categoria);
        return CreatedAtAction(nameof(categoria), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(int id, Categoria categoria)
    {
        if (id != categoria.Id) return BadRequest();

        await _categoriaRepository.AtualizarAsync(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _categoriaRepository.DeletarAsync(id);
        return Ok();
    }
}