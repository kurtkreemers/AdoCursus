using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdoWpf
{
    public class PostcodeRangeRule2:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int postcode = 0;
            try
            {
                if (((string)value).Length > 0) postcode = Int16.Parse((String)value);
            }
            catch (Exception e)
            {

                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }
            if ((postcode < 1000) || (postcode > 9999))
            {
                return new ValidationResult(false, "De postcode moet > 999 en < 10000 zijn");
            }
            else
                return new ValidationResult(true, null);
        }
    }
}
