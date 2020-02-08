using System;

namespace payrollCaseStudy
{
    public class MonthlySchedule : PaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }

        public bool IsPayDay(DateTime payDate)
        {
            return IsLastDayOfMonth(payDate);
        }

    }
}