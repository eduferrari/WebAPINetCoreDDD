using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Application.Interfaces;
public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ListarTodosAsync();
    Task<Categoria> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Categoria categoria);
    Task AtualizarAsync(Categoria categoria);
    Task DeletarAsync(int id);
}