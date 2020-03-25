using System;

namespace payrollCaseStudy
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDay(DateTime payDate)
        {
            return IsFriday(payDate);
        }

        public DateTime GetStartDate(DateTime payDate)
        {
            return payDate.AddDays(-5);
        }

        private static bool IsFriday(DateTime payDate)
        {
            return payDate.DayOfWeek.Equals(DayOfWeek.Friday);
        }
    }
}