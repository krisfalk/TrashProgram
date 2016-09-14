using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram.Models
{
    public class UserInfo
    {
        public UserInfo()
        {

        }
        [Key]
        public int? UserInfo_id { get; set; }
        public string PickupDay { get; set; }
        public double MonthlyBill { get; set; }
        public double YearlyBill { get; set; }
        public double TotalBill { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}