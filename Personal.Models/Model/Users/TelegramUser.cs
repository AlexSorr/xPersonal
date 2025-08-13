using System;
using System.ComponentModel.DataAnnotations.Schema;
using Personal.Models.Model.Base;

namespace Personal.Models.Model.Users;

/// <summary>
/// Аккаунт пользователя в телеграмме
/// </summary>
public class TelegramUser : UserAttribute {

    public long TelegramId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Firstname { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

}
