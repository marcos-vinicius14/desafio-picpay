using System.ComponentModel.DataAnnotations;
using Picpay_01.Models.Enums;

namespace Picpay_01.ViewModels;

public class UserViewModel
{
    //public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve conter no minímo 3 caracteres e no máximo 40")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve conter no minímo 3 caracteres e no máximo 40")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
    [StringLength(16, ErrorMessage = "CPF/CNPJ deve conter no máximo 16 caracteres.")]
    public string Document { get; set; }

    public double Balance { get; set; } = 0.0;
    
    [Required(ErrorMessage = "Informe um E-mail.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Crie uma senha")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "A senha deve conter no minimo 8 caracteres")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Informe o tipo de conta.")]
    public UserType UserType { get; set; }
}