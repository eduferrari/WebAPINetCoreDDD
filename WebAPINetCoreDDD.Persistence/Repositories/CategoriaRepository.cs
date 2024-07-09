using Microsoft.EntityFrameworkCore;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Persistence.Repositories;
public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> ListarTodosAsync()
    {
        return await _context.Categorias.Where(c => c.Status != Status.Deletado).ToListAsync();
    }

    public async Task<Categoria> BuscarPorIdAsync(int id)
    {
        return await _context.Categorias.Where(x => x.Status != Status.Deletado && x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if(categoria is null)
        {
            categoria.Status = Status.Deletado;

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
