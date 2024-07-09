using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Application.Interfaces;
public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ListarTodosAsync();
    Task<Produto> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task DeletarAsync(int id);
}
