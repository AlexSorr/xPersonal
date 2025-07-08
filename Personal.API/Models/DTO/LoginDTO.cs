using System;

namespace Personal.API.Models.DTO;

/// <summary>
/// DTO для логина в приложение
/// </summary>
public class LoginDTO {

    /// <summary>
    /// имя пользователя
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;

}
