using Microsoft.Identity.Client;
using Service.DTOs.Account;
using Service.DTOs.AccountDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<CreateResponse> CreateRoles();
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<RoleDTO>> GetRoles();
        Task<LoginResponse> LoginAsync(LoginDTO entity);
        Task<CreateResponse> AddRole(string UserId, string RoleId);
        Task<CreateResponse> RemoveRole(string UserId, string RoleId);
    }
}
