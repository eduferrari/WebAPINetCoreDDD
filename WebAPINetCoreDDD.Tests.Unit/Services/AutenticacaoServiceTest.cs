using Microsoft.Extensions.Configuration;
using Moq;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;
using WebAPINetCoreDDD.Infrastructure.Services;

namespace WebAPINetCoreDDD.Tests.Unit.Services;
public class AutenticacaoServiceTest
{
    private readonly Mock<IUsuarioRepository> _mockUserRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AutenticacaoService _authService;

    public AutenticacaoServiceTest()
    {
        _mockUserRepository = new Mock<IUsuarioRepository>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJOb21lIjoiRWR1YXJkbyBGZXJyYXJpIiwiRW1haWwiOiJjb250YXRvQGRhcmtvY29kZS5jb20uYnIiLCJSb2xlIjoiQWRtaW4ifQ.j_OSar4PfTzRgzZJYlu79hTxz8lLi3bEnTVYmpa-wcs");
        _authService = new AutenticacaoService(_mockUserRepository.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task Autenticacao_UsuarioValido_RetornaToken()
    {
        // Arrange
        var user = new Usuario { Id = 1, Nome = "test", Email = "test@test.com", Senha = "test", Perfil = "Admin", Status = Status.Ativo };
        _mockUserRepository.Setup(r => r.ValidarLoginAsync(user.Email, user.Senha)).ReturnsAsync(user);

        // Act
        var token = await _authService.Autenticacao(user.Email, user.Senha);

        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public async Task Autenticacao_UsuarioInvalido_RetornaNulo()
    {
        // Arrange
        var user = new Usuario { Email = "invalid", Senha = "invalid" };
        _mockUserRepository.Setup(r => r.ValidarLoginAsync(user.Email, user.Senha)).ReturnsAsync(user);

        // Act
        var token = await _authService.Autenticacao(user.Email, user.Senha);

        // Assert
        Assert.Null(token);
    }
}