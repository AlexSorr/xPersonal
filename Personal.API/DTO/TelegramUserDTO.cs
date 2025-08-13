using System;

namespace Personal.API.DTO;

/// <summary>
/// DTO для пользователя TG
/// </summary>
public class TelegramUserDTO {

    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

}
