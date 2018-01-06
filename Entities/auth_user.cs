using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class auth_user
    {
        public int id { get; set; }
        public string username { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public int employee_id { get; set; }
        [Required]
        public int auth_user_role_id { get; set; }
        public int auth_user_level { get; set; }
    }
}