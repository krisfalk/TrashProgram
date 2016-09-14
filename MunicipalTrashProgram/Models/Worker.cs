using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram.Models
{
    public class Worker
    {
        public Worker(ApplicationUser User)
        {
            this.User = User;
        }
        [Key]
        public int Worker_id { get; set; }
        public int WorkingZipCode { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}