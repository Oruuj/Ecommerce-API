using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers.Admin
{
    [Authorize(Roles = "Admin,SuperAdmin,Moderator")]
    [Route("api/[controller]/Admin/[action]")]
    [ApiController]
    public class AdminController : ControllerBase { }
}
