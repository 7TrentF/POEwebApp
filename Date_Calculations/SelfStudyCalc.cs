using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date_Calculations
{
    public class SelfStudyCalc
    {
        public double SsCalc(double Credits, double WeeksInSemester, double ClassHours)
        {
            double selfStudyHrs = (Credits * 10) / WeeksInSemester - ClassHours;
            return selfStudyHrs;
        }
    }
}
