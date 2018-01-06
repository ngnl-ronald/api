using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id{ get; set; }

        //[Required]
        public string code { get; set; }

        //[Required]
        public string given_name { get; set; }

        //[Required]
        public string surname { get; set; }

        public string other_given_name { get; set; }

        public string prefix { get; set; }

        public string suffix { get; set; }

        public string tfn { get; set; }

        //[Phone]
        public string mobile_number { get; set; }

        //[EmailAddress]
        public string email_address { get; set; }
       
        public DateTime data_of_birth { get; set; }

        public int gender_id { get; set; }

        public int work_type_id { get; set; }

        public int employee_status_id { get; set; }

        //[Required]
        public int created_by_id { get; set; }

        //[Required]
        public DateTime date_created { get; set; }

        public int modified_by_id { get; set; }

        public DateTime date_modified { get; set; }
    }
}