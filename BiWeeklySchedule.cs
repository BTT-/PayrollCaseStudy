using System;

namespace payrollCaseStudy
{
    public class BiWeeklySchedule : PaymentSchedule
    {
        public bool IsPayDay(DateTime payDate)
        {
            return false;
        }
    }
}