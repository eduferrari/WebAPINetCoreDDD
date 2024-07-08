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
        return await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email && u.Senha == senha);
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> BuscarPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
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
        var user = await _context.Usuarios.FindAsync(id);
        if (user != null)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
