using Domain.Entities;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Service.DTOs.Account;
using Service.DTOs.AccountDTOs;
using Service.Helpers.Enums;
using Service.Services.Interfaces;
using System.Security.Claims;

namespace Ecommerce_API.Controllers.UI
{
    public class AccountController : UIController
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return BadRequest(ModelState);

            }
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("orujnov11@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Email Confirmation";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $@"
    <html>
    <head>
        <style>
            .email-container {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
            }}
            .email-content {{
                max-width: 600px;
                margin: auto;
                background-color: #ffffff;
                padding: 30px;
                border-radius: 8px;
                box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            .button {{
                display: inline-block;
                padding: 12px 24px;
                margin-top: 20px;
                background-color: #007bff;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold;
            }}
            .footer {{
                margin-top: 30px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-content'>
                <h2>Welcome to Our Store!</h2>
                <p>Hi {request.FullName},</p>
                <p>Thank you for registering. Please confirm your email by clicking the button below:</p>
                <a class='button' href='{url}'>Confirm Email</a>
                <p class='footer'>If you didn’t create this account, you can ignore this email.</p>
            </div>
        </div>
    </body>
    </html>"
            };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("orujnov11@gmail.com", "ncic ebko jdra rkgj");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser existUSer = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(existUSer, token);
            return Content(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Email Confirmation</title>
    <style>
        body {
            background-color: #f4f4f4;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 40px;
            text-align: center;
        }
        .container {
            background-color: white;
            max-width: 500px;
            margin: auto;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }
        h2 {
            color: #28a745;
            margin-bottom: 10px;
        }
        p {
            font-size: 16px;
            color: #555;
        }
        .button {
            display: inline-block;
            margin-top: 20px;
            padding: 12px 24px;
            background-color: #007bff;
            color: white;
            text-decoration: none;
            border-radius: 6px;
            font-weight: bold;
        }
        .button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h2>✅ Email Confirmed Successfully!</h2>
        <p>Your account is now activated. You can now log in and start shopping.</p>
        <a class='button' href='http://localhost:5173/account'>Go to Login Page</a>
    </div>
</body>
</html>
", "text/html");
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO request) => Ok(await _accountService.LoginAsync(request));
        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            var result = await _accountService.CreateRoles();
            return Ok(result);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FullName
            });
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (user.Email != entity.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(entity.Email);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return BadRequest(new { message = "Email is already in use by another user." });
                }

                user.Email = entity.Email;

                user.EmailConfirmed = false;

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("orujnov11@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Email Confirmation";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $@"
    <html>
    <head>
        <style>
            .email-container {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
            }}
            .email-content {{
                max-width: 600px;
                margin: auto;
                background-color: #ffffff;
                padding: 30px;
                border-radius: 8px;
                box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            .button {{
                display: inline-block;
                padding: 12px 24px;
                margin-top: 20px;
                background-color: #007bff;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold;
            }}
            .footer {{
                margin-top: 30px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-content'>
                <h2>Welcome to Our Store!</h2>
                <p>Hi {entity.FullName},</p>
                <p>Thank you for registering. Please confirm your email by clicking the button below:</p>
                <a class='button' href='{url}'>Confirm Email</a>
                <p class='footer'>If you didn’t create this account, you can ignore this email.</p>
            </div>
        </div>
    </body>
    </html>"
                };
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("orujnov11@gmail.com", "ncic ebko jdra rkgj");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            user.FullName = entity.FullName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return BadRequest(ModelState);
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FullName
            });
        }

    }
}
