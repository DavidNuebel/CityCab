using System;
using System.Collections.Generic;
using System.Text;

namespace CityCab.Libraries.Dto.Authentication
{
    public class LogoutResponse : EventArgs
    {
        public bool State { get; set; }
    }
}
