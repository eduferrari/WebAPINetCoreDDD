using Microsoft.EntityFrameworkCore;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Persistence.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> ValidarLoginAsync(string email, string senha)
    {
        return await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == email && x.Senha == senha && x.Status != Status.Deletado);
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAsync()
    {
        return await _context.Usuarios.Where(x => x.Status != Status.Deletado).ToListAsync();
    }

    public async Task<Usuario> BuscarPorIdAsync(int id)
    {
        return await _context.Usuarios.Where(x => x.Status != Status.Deletado && x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }


    public async Task DeletarAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            usuario.Status = Status.Deletado;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> VerificaSeUsuarioJaCadastrado(string username)
    {
        return await _context.Usuarios.Where(r=> r.Email == username && r.Status != Status.Deletado).AnyAsync();
    }
}
