using Microsoft.Extensions.Logging;
using Personal.Data;
using Personal.Model.Users;
using Personal.Service.Services.Interfaces;
using Personal.Services.Base;

namespace Personal.Service.Services.Base;

public class UserService : EntityService<User>, IUserService {

    public UserService(ApplicationDbContext context, ILogger<EntityService<User>> logger, IEntityServiceFactory serviceFactory) : base(context, logger, serviceFactory) { }

    /// <inheritdoc/>
    public Task<long> RegistrateUser(User user) {
        if (user == null) return Task.FromResult(0L);
    }

}
