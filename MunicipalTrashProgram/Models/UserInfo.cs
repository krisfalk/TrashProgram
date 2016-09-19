using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int VacationDays { get; set; }
    }
}