using Azure.Core;
using Domain.Entities;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit;
using MimeKit.Text;
using MimeKit.Text;
using Service.DTOs.Account;
using Service.DTOs.AccountDTOs;
using Service.Helpers;
using Service.Helpers.Enums;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSetting _jwtSetting;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSetting> options, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSetting = options.Value;
            _configuration = configuration;
        }

        public async Task<CreateResponse> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
            return new CreateResponse { StatusCode = 200, Message = "Roles created successfully" };
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = [];

            var dbUsers = await _userManager.Users.ToListAsync();
            foreach (var user in dbUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                users.Add(new UserDTO
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Id=user.Id,
                    Roles = userRoles.ToArray()
                });
            }
            return users;
        }

        public async Task<IEnumerable<RoleDTO>> GetRoles()
        {
            List<RoleDTO> roles = [];

            var dbRoles = await _roleManager.Roles.ToListAsync();

            foreach (var item in dbRoles)
            {
                var users = await _userManager.GetUsersInRoleAsync(item.ToString());
                roles.Add(new RoleDTO
                {
                    Name = item.Name,
                    Users = users.Select(m => new UserDTO
                    {
                        FullName = m.FullName,
                        Email = m.Email,
                        UserName = m.UserName,
                        Id = m.Id,
                    }).ToArray()
                });
            }
            return roles;
        }
        public async Task<LoginResponse> LoginAsync(LoginDTO entity)
        {
            AppUser? user = await _userManager.FindByEmailAsync(entity.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(entity.UserNameOrEmail);
            if (user == null)
                return new LoginResponse { Succes = false, Token = null, ErrorMessage = "no user found" };

            bool result = await _userManager.CheckPasswordAsync(user, entity.Password);

            if (!result)
                return new LoginResponse { Succes = false, Token = null, ErrorMessage = "no user found" };

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateJwtToken(user.Id, roles.ToList());

            return new LoginResponse { Succes = true, ErrorMessage = null, Token = token ,UserId=user.Id};
        }

        public async Task<RegisterResponse> Register(RegisterDTO entity)
        {
            AppUser user = new AppUser()
            {
                FullName = entity.FullName,
                Email = entity.Email,
                UserName = entity.UserName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(m => m.Description).ToArray()
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            return new RegisterResponse { Success = true, Errors = null };
        }

        public async Task<CreateResponse> AddRole(string UserId, string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                throw new NullReferenceException("No User Found");

            var role = await _roleManager.FindByNameAsync(RoleId);
            if (role == null)
                throw new NullReferenceException("No Role Found");
            await _userManager.AddToRoleAsync(user, role.ToString());
            return new CreateResponse { StatusCode = 200, Message = "Role added successfully" };
        }
        public async Task<CreateResponse> RemoveRole(string UserId, string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                throw new NullReferenceException("No User Found");
            var role = await _roleManager.FindByNameAsync(RoleId);
            if (role == null)
                throw new NullReferenceException("No Role Found");
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count <= 1)
                throw new Exception("A user must have at least one role");
            if (role.ToString().ToLower() == "user")
                throw new Exception("Can't remove member role");
            await _userManager.RemoveFromRoleAsync(user, role.ToString());
            return new CreateResponse { StatusCode = 200, Message = "Role removed successfully" };
        }

        private string GenerateJwtToken(string userid, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userid),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, userid)
        };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSetting.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}