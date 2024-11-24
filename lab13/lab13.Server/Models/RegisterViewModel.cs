using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Поле ім'я користувача обов'язкове")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9_-]+$")]
    [Remote("IsUsernameAvailable", "Account")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Поле ПІБ обов'язкове")]
    [RegularExpression(@"^[А-ЯІЇЄҐа-яіїєґ'\s-]+$")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Поле пароль обов'язкове")]
    [StringLength(16, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Необхідно підтвердити пароль")]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Поле телефон обов'язкове")]
    [RegularExpression(@"^\+38(0\d{9})$")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Поле email обов'язкове")]
    [EmailAddress]
    public string Email { get; set; }
}