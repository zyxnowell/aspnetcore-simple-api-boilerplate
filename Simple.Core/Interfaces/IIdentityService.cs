using Microsoft.AspNetCore.Identity;
using Simple.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple.Core.Interfaces
{
    public interface IIdentityService
    {
        Task<BoolResult> AddUser(SetUserModel model);
        Task<List<IdentityRole>> GetAllRoles();
        Task<AuthenticateResult> Authenticate(AuthenticateModel inputs);
    }
}
