using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class AuthUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        //[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        public int SecurityUserRole_Id { get; set; }
        public int SecurityUserGroup_Id { get; set; }

    }
}