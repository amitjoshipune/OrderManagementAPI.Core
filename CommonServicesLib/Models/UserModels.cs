using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Models
{
    //public class User
    //{
    //    [Key]
    //    public string Id { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    [AllowNull]
    //    public string EmailId { get; set; }
    //}
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public Guid LoginId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
