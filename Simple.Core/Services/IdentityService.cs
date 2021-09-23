using Simple.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Intrastructure.Context;
using Simple.Intrastructure.Entities;
using Simple.Core.Models;

namespace Simple.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppDBContext _dbContext;

        public IdentityService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            AppDBContext dbContext)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<BoolResult> AddUser(SetUserModel model)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);

                if (role == null)
                    return new BoolResult(false, "Role does not exists");

                var newUser = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    CreatedAt = DateTime.UtcNow,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsArchived = false
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(model.Username);

                    var addRole = await _userManager.AddToRoleAsync(user, role.Name);

                    return new BoolResult(true, "Add user success");
                }


                return new BoolResult(false, "Add user failed");
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {

            try
            {
                var roles = _roleManager.Roles.ToList();

                return roles;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
