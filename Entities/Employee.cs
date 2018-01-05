using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }

        //[Required]
        public string Code { get; set; }

        //[Required]
        public string GivenName { get; set; }

        //[Required]
        public string Surname { get; set; }

        public string OtherGivenName { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string TFN { get; set; }

        //[Phone]
        public string MobileNumber { get; set; }

        //[EmailAddress]
        public string EmailAddress { get; set; }
       
        public DateTime DateOfBirth { get; set; }

        public int Gender_Id { get; set; }

        public int WorkType_Id { get; set; }

        public int EmployeeStatus_Id { get; set; }

        //[Required]
        public int CreatedBy_Id { get; set; }

        //[Required]
        public DateTime DateCreated { get; set; }

        public int ModifiedBy_Id { get; set; }

        public DateTime DateModified { get; set; }
    }
}