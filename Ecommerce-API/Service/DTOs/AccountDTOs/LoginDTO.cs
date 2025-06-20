using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Account
{
    public class LoginDTO
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
