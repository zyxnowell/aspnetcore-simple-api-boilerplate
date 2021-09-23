using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Core.Models
{
    public class CommonUserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SetUserModel : CommonUserModel
    {
        public string Password { get; set; }
    }
}
