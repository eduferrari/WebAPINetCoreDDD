using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Application.Interfaces;
public interface IUsuarioRepository
{
    Task<Usuario> ValidarLoginAsync(string username, string password);
    Task<IEnumerable<Usuario>> ListarTodosAsync();
    Task<bool> VerificaSeUsuarioJaCadastrado(string username);
    Task<Usuario> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task DeletarAsync(int id);
}
