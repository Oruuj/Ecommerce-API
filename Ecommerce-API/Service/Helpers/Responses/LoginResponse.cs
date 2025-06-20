using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class LoginResponse
    {
        public bool Succes { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public string? UserId { get; set; } = null;
    }
}
