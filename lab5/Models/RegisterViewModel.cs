using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Поле ім'я користувача обов'язкове")]
    [StringLength(50, ErrorMessage = "Ім'я користувача не може перевищувати 50 символів")]
    [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Ім'я користувача може містити лише літери, цифри, дефіс та підкреслення")]
    [Remote("IsUsernameAvailable", "Account", ErrorMessage = "Це ім'я користувача вже зайняте")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Поле ПІБ обов'язкове")]
    [StringLength(500, ErrorMessage = "ПІБ не може перевищувати 500 символів")]
    [RegularExpression(@"^[А-ЯІЇЄҐа-яіїєґ'\s-]+$", ErrorMessage = "ПІБ може містити лише українські літери, апостроф, дефіс та пробіли")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Поле пароль обов'язкове")]
    [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль повинен бути від 8 до 16 символів")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$",
        ErrorMessage = "Пароль повинен містити принаймні одну велику літеру, одну цифру та один спеціальний символ")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Необхідно підтвердити пароль")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Поле телефон обов'язкове")]
    [RegularExpression(@"^\+38(0\d{9})$", ErrorMessage = "Введіть коректний український номер телефону у форматі +380XXXXXXXXX")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Поле email обов'язкове")]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        ErrorMessage = "Некоректний формат email")]
    public string Email { get; set; }
}