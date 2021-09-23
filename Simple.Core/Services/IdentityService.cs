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
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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


        /// <summary>
        /// Use this to generate JWT to authenticate user in the api 
        /// </summary>
        public async Task<AuthenticateResult> Authenticate(AuthenticateModel inputs)
        {

            try
            {
                var normalizedUserName = inputs.UserName.ToUpper();

                var signin = await _signInManager.PasswordSignInAsync(normalizedUserName, inputs.Password, false, false);

                if (signin.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(normalizedUserName);

                    if (user.IsArchived)
                        return null;

                    var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(
                        user,
                        inputs.Password,
                        false);

                    if (checkPasswordResult.Succeeded)
                    {

                        var jwtToken = await GenerateToken(user, inputs.KeepLoggedIn);

                        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                        return new AuthenticateResult
                        {
                            Token = jwtToken,
                            Email = user.Email,
                            FullName = $"{user.FirstName} {user.LastName}",
                            Role = role
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        #region private methods
        private async Task<string> GenerateToken(AppUser user, bool KeepLoggedIn = false)
        {
            var utcStart = DateTime.UtcNow;

            //INFO: 7 days expiry if keepLoggedIn, else 1 day
            var expiration = utcStart.AddDays(KeepLoggedIn ? Convert.ToInt32(7) : Convert.ToInt32(1));

            var claims = await GetClaims(user);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _config["Jwt:Issuer"];

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = credentials,
                IssuedAt = DateTime.Now
            };


            var tokenHandler = new JwtSecurityTokenHandler();


            var token = tokenHandler.WriteToken((JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }

        private async Task<List<Claim>> GetClaims(AppUser user)
        {

            var roles = await _userManager.GetRolesAsync(user);

            var claims = await _userManager.GetClaimsAsync(user);

            var claimIdentities = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {

                claimIdentities.Add(new Claim(ClaimTypes.Role, role));

                claimIdentities.AddRange(claims.Select(claim => new Claim(claim.Type, claim.Value)));
            }

            return claimIdentities;
        }
        #endregion
    }
}
