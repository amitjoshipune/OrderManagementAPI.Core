using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Models
{
    //public class UserDto
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }

    //    [AllowNull]
    //    public string EmailId { get; set; }
    //}

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
