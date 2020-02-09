using System;

namespace payrollCaseStudy
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDay(DateTime payDate)
        {
            return IsFriday(payDate);
        }

        private static bool IsFriday(DateTime payDate)
        {
            return payDate.DayOfWeek.Equals(DayOfWeek.Friday);
        }
    }
}