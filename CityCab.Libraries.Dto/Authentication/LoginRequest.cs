using System;
using System.Collections.Generic;
using System.Text;

namespace CityCab.Libraries.Dto.Authentication
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
