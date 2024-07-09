using Microsoft.EntityFrameworkCore;
using WebAPINetCoreDDD.Application.Interfaces;
using WebAPINetCoreDDD.Domain.Entities;

namespace WebAPINetCoreDDD.Persistence.Repositories;
public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ListarTodosAsync()
    {
        return await _context.Produtos.Where(x => x.Status != Status.Deletado).ToListAsync();
    }

    public async Task<Produto> BuscarPorIdAsync(int id)
    {
        return await _context.Produtos.Where(x => x.Status != Status.Deletado && x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null)
        {
            produto.Status = Status.Deletado;

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}
