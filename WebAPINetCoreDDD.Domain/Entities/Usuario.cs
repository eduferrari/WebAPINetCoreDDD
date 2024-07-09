﻿using System.ComponentModel.DataAnnotations;

namespace WebAPINetCoreDDD.Domain.Entities;
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Perfil { get; set; }

    [AllowedValues("Ativo", "Inativo", "Deletado")]
    public Status Status { get; set; }
}