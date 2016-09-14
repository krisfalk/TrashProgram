using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram.Models
{
    public class Address
    {
        public Address()
        {
        }
        [Key]
        public string Address_id { get; set; }
        public int HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}