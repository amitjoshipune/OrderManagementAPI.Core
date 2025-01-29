namespace YourNamespace.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}