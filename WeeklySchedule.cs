using System;

namespace payrollCaseStudy
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDay(DateTime payDate)
        {
            return false;
        }   
    }
}