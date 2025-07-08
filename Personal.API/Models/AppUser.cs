using System;
using Microsoft.AspNetCore.Identity;

namespace Personal.API.Models;

/// <summary>
/// Пользователь приложения, класс для хранения учетных данных
/// </summary>
public class AppUser : IdentityUser<long> {

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    private string _password = string.Empty;
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get => _password; }

    /// <summary>
    ///  Сменить пароль
    /// TODO Реализовать обновление пароля
    /// </summary>
    public void UpdatePassword() { }

}
