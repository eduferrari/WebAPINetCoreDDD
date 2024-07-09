using System.ComponentModel.DataAnnotations;

namespace WebAPINetCoreDDD.Domain.Entities;
public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Status Status { get; set; }
}
