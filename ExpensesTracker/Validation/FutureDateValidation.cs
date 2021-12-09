using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesTracker.Validation
{
    public class FutureDateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var fdate = DateTime.Parse(value.ToString());
            return fdate.Date <= DateTime.Now.Date;
        }
    }
}
