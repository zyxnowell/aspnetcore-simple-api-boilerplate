using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Core.Models
{
    public class AuthenticateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; } = false;
    }

    public class AuthenticateResult
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsArchived { get; set; } = false;
        public string Role { get; set; }
    }
}
