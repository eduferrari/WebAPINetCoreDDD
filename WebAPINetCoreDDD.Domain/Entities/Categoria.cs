namespace WebAPINetCoreDDD.Domain.Entities;
public class Categoria
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public bool Ativo { get; set; }
    public bool Deletado { get; set; }
}
