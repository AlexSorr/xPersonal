using System;
using Microsoft.AspNetCore.Mvc;
using Personal.API.DTO;
using Personal.Models.Model.Users;
using Personal.Services.Extensions;
using Personal.Services.ExtensionPonints;

namespace Personal.API.Controllers;

public class UserController : APIBaseController<User> {

    protected readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger) {
        _userService = userService;
    }


    /// <summary>
    /// Пользователь из Telegram Существует
    /// </summary>
    /// <param name="telegramUserId">user_id из Telegram</param>
    [HttpGet("tg_user_exists_{telegramUserId}")]
    public async Task<ActionResult<bool>> TelegramUserExists(long telegramUserId) {
        return await _userService.TelegramUserExistsAsync(telegramUserId);
    }


    /// <summary>
    /// Зарегистрировать пользователя по данным Telegram
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> RegistrateUser([FromBody] TelegramUserDTO userDTO) {

        var tg_user = userDTO.CreateFromDTO();
        var regResult = await _userService.RegistrateUserAsync(tg_user);

        if (!regResult.Success)
            return BadRequest($"Ошибка при регистрации пользователя: {regResult.Description}");

        return Ok();

    }

}
