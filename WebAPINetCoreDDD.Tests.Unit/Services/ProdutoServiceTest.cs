using Moq;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Tests.Unit.Services;
public class ProdutoServiceTest
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly List<Produto> _produtos;

    public ProdutoServiceTest()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _produtos = [
            new Produto { Id = 1, CategoriaId = 1, Nome = "Produto teste 01", Descricao = "Descrição teste 01", Status = Status.Ativo },
            new Produto { Id = 2, CategoriaId = 2, Nome = "Produto teste 02", Descricao = "Descrição teste 02", Status = Status.Ativo },
            new Produto { Id = 3, CategoriaId = 1, Nome = "Produto teste 03", Descricao = "Descrição teste 03", Status = Status.Inativo }
        ];
    }

    [Fact]
    public async Task RetornarTodosProdutos()
    {
        // Arrange
        _produtoRepositoryMock.Setup(repo => repo.ListarTodosAsync()).ReturnsAsync(_produtos);

        // Act
        var result = await _produtoRepositoryMock.Object.ListarTodosAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task RetornarProdutoPeloId()
    {
        // Arrange
        var produtoId = 1;
        _produtoRepositoryMock.Setup(repo => repo.BuscarPorIdAsync(produtoId)).ReturnsAsync(_produtos.First(p => p.Id == produtoId));

        // Act
        var result = await _produtoRepositoryMock.Object.BuscarPorIdAsync(produtoId);

        // Assert
        Assert.Equal("Produto teste 01", result.Nome);
    }

    [Fact]
    public async Task AdicionaProduto()
    {
        // Arrange
        var produto = new Produto { Id = 4, Nome = "Produto teste 04", Descricao = "Descrição teste 04", Status = Status.Ativo };
        _produtoRepositoryMock.Setup(repo => repo.AdicionarAsync(produto)).Callback(() => _produtos.Add(produto));

        // Act
        await _produtoRepositoryMock.Object.AdicionarAsync(produto);

        // Assert
        Assert.Contains(produto, _produtos);
    }

    [Fact]
    public async Task AtualizaProduto()
    {
        // Arrange
        var produto = _produtos.First();
        produto.Nome = "Produto teste 01 (atualizado)";
        _produtoRepositoryMock.Setup(repo => repo.AtualizarAsync(produto)).Callback(() =>
        {
            var product = _produtos.First(p => p.Id == produto.Id);
            product.Nome = produto.Nome;
        });

        // Act
        await _produtoRepositoryMock.Object.AtualizarAsync(produto);

        // Assert
        Assert.Equal("Produto teste 01 (atualizado)", _produtos.First(p => p.Id == produto.Id).Nome);
    }

    [Fact]
    public async Task DeleteProduct_DeletesProduct()
    {
        // Arrange
        var produtoId = 1;
        _produtoRepositoryMock.Setup(repo => repo.DeletarAsync(produtoId)).Callback(() =>
        {
            var product = _produtos.First(p => p.Id == produtoId);
            _produtos.Remove(product);
        });

        // Act
        await _produtoRepositoryMock.Object.DeletarAsync(produtoId);

        // Assert
        Assert.DoesNotContain(_produtos, p => p.Id == produtoId);
    }
}
