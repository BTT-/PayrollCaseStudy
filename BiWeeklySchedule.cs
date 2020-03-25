using System;
using System.Globalization;

namespace payrollCaseStudy
{
    public class BiWeeklySchedule : PaymentSchedule
    {
        public bool IsPayDay(DateTime payDate)
        {
            var calender = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            var week = calender.GetWeekOfYear(payDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return (week % 2 == 0) && payDate.DayOfWeek == DayOfWeek.Friday;
        }

        public DateTime GetStartDate(DateTime payDate)
        {
            return payDate.AddDays(-13);
        }
    }
}