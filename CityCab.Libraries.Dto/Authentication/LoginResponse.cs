using System;
using System.Collections.Generic;
using System.Text;

namespace CityCab.Libraries.Dto.Authentication
{
    public class LoginResponse : EventArgs
    {
        public int AccountID { get; set; }
        public Guid AccessToken { get; set; }
        public string ErrorCode { get; set; }
    }
}
