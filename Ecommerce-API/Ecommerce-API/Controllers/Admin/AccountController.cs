using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class AccountController : AdminController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoles();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _accountService.GetAllUsers());
        }
        [HttpGet]
        public async Task<IActionResult> GetRolesWithUsers()
        {
            return Ok(await _accountService.GetRoles());
        }
        [HttpPut]
        public async Task<IActionResult> AddRoleToUser([FromQuery] string UserId, [FromQuery] string RoleId)
        {
            await _accountService.AddRole(UserId, RoleId);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery] string UserId, [FromQuery] string RoleId)
        {
            await _accountService.RemoveRole(UserId, RoleId);
            return Ok();
        }
    }
}
