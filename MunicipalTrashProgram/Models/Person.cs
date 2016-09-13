using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram.Models
{
    public class Person
    {
        public Person()
        {

        }
        [Key]
        public int Person_id { get; set; }
        [ForeignKey("Address")]
        public int Address_id { get; set; }
        public Address Address { get; set; }
        [ForeignKey("Worker")]
        public int? Worker_id { get; set; }
        public Worker Worker { get; set; }
        [ForeignKey("UserInfo")]
        public int? UserInfo_id { get; set; }
        public UserInfo UserInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string _Email { get; set; }

    }
}