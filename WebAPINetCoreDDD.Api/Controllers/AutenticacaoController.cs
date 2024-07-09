using Microsoft.AspNetCore.Mvc;
using WebAPINetCoreDDD.Application.Interfaces;

namespace WebAPINetCoreDDD.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AutenticacaoController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AutenticacaoModel autenticacao)
    {
        var token = await _autenticacaoService.Autenticacao(autenticacao.Email, autenticacao.Senha);
        if (token == null) return Unauthorized();

        return Ok(new { Token = token });
    }

    public class AutenticacaoModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
