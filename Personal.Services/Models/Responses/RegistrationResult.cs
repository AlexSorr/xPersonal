using System;
using Personal.Models.Model.Users;

namespace Personal.Services.Models.Responses;

public class RegistrationResult {

    public RegistrationResult(User user) => User = user;

    public User User { get; set; }

    public bool Success { get; set; }

    public string Description { get; set; } = string.Empty;
 
}
