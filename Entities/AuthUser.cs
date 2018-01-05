using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Employee_Id { get; set; }
        [Required]
        public int SecurityUserRole_Id { get; set; }
        public int SecurityUserLevel_Id { get; set; }
    }
}