using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [NotMapped]
    public class Lookup
    {
        [Key]
        public int lookup_id { get; set; }
        public string lookup_name { get; set; }
    }
}
