using System;
using Personal.Models.Model.Users;

namespace Personal.API.DTO;

public static class DTOExtensions {

    public static TelegramUser CreateFromDTO(this TelegramUserDTO dto) {
        if (dto == null)
            return null;

        return new TelegramUser() {
            TelegramId = dto.TelegramId,
            Username = dto.Username,
            Firstname = dto.FirstName,
            LastName = dto.LastName
        };
    }

}
