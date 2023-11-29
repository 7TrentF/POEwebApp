using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date_Calculations
{
    public class ModuleClass
    {
        // Properties to store module information.

        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public double ModuleCredits { get; set; }
        public double ClassHours { get; set; }
        public double WeeksInSemester { get; set; }
        public DateTime StartDate { get; set; }
        public double Selfstudy { get; set; }

        // Constructor to initialize the ModuleClass with module information.
        public ModuleClass(string code, string moduleName, double credits, double classHrs, double weeksInSemester, DateTime startDate, double selfStudy)
        {
            ModuleCode = code;                  // Initialize the module code property.
            ModuleName = moduleName;            // Initialize the module name property.
            ModuleCredits = credits;            // Initialize the module credits property.
            ClassHours = classHrs;              // Initialize the class hours property.
            StartDate = startDate;              // Initialize the start date property.
            WeeksInSemester = weeksInSemester;  // Initialize the weeks in semester property.
            Selfstudy = selfStudy;              // Initialize the self-study property.
        }
        /*
          Author: Doyle, B. (2016) 
          title of the book: C♯ Programming: From problem analysis to program design. Boston, MA: Cengage Learning. pg 475-484
          accessed:  15 September 2023
        */
    }
}
