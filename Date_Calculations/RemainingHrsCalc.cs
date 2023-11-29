using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Date_Calculations
{
    public class RemainingHrsCalc
    {
        public double remaininghrs;

        public DateTime CheckDate(DateTime startDate, DateTime StudyDate)
        {
            DateTime currentWeekEndDate = startDate.AddDays(7);  // set  the value of the currentWeekEndDate to be 7 days from  the start date
            bool currentWeekCheck = StudyDate >= startDate && StudyDate <= currentWeekEndDate; // Calculate the end date of the current week based on the module's start date.
         
            if (!currentWeekCheck) // Check if the study date does not fall within the current week
            {
               
                //  return DateTime.MinValue; // or any other special value to indicate that the check failed
                throw new Exception("The end date must be within the current week !!!"); //throw an exception to indicate an error
            }


            /*
             * Author: RohitPrasad3
             * Website: https://www.geeksforgeeks.org/datetime-adddays-method-in-c-sharp/
             * Accessed: 19 September 2023
             * 
             */

            return currentWeekEndDate;
        }

        public DateTime CheckWorkDate(DateTime workDate, DateTime studyDate)
        {
            DateTime currentWeekEndDate = workDate.AddDays(7); // set the value of the currentWeekEndDate to be 7 days from the workDate
            bool currentWeekCheck = studyDate >= workDate && studyDate <= currentWeekEndDate; // Calculate the end date of the current week based on the workDate.
            
            if (!currentWeekCheck) // Check if the study date does not fall within the current week
            {
                throw new Exception("The selected date must be within the current week !!!"); //throw an exception to indicate an error
            }
            /*
         * Author: RohitPrasad3
         * Website: https://www.geeksforgeeks.org/datetime-adddays-method-in-c-sharp/
         * Accessed: 19 September 2023
         * 
         */

            return currentWeekEndDate;
        }


        public double CalculateRemainingSelfStudyHoursForCurrentWeek(string code, double selfstudy, double hrsSpent, DateTime workDate)
        {
            if (hrsSpent > selfstudy) //Check if the Hours Spent on module is greater than the self study hours
            {

                throw new Exception($"Hours spent on the module cannot be more than: {selfstudy}."); //throw an exception to indicate an error

            }
            else
            {
                remaininghrs = selfstudy - hrsSpent;
                return remaininghrs;
            }
        }

        public double CalculateRemainingHours(string moduleCode, double hrsSpent, double existingRemainingHrs)
        {

            if (hrsSpent > existingRemainingHrs)
            {
                throw new Exception($"Hours spent on the module cannot be more than: {existingRemainingHrs}."); //throw an exception to indicate an error
            }

            else
            {
                double remainingHrs = existingRemainingHrs - hrsSpent;
                return remainingHrs;
            }

        }

    }
}
