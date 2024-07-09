using System.ComponentModel.DataAnnotations;

namespace WebAPINetCoreDDD.Domain.Entities;
public class Produto
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Status Status { get; set; }

    public Categoria Categoria { get; set; }
}
