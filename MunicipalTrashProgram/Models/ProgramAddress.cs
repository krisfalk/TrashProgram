using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram.Models
{
    public class ProgramAddress
    {
        public ProgramAddress()
        {

        }
        public string description { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
    }
}