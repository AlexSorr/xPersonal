using System;

namespace Personal.API.Models.DTO;

/// <summary>
/// DTO с данными для регистрации нового пользователя
/// </summary>
public class RegisterDTO {

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    public string ConfirmPassword { get; set; } = string.Empty;

}
