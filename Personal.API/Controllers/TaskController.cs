using Personal.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Model.Task;
using Personal.Services.Base;

namespace Personal.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с задачей <see cref="TaskBase"/>
/// </summary>
[Route("api/taskbase")]
[ApiController]
public class TaskBaseController : APIBaseController<TaskBase> {
    
    public TaskBaseController(ILogger<APIBaseController<TaskBase>> logger, IEntityService<TaskBase> entityService) : base(logger, entityService) { }

    #region POST

    public async Task<ActionResult<long>> CreateTask(long userId, string name, string description) {
        
    }

    #endregion

}