﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram
{
    public class DoWork
    {
        public DoWork()
        {

        }
        public double ComputeBill(int numberOfBills, double weeklyCost)
        {
            return numberOfBills * weeklyCost;
        }
    }
}