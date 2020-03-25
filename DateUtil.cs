using System;

namespace payrollCaseStudy
{
    public static class DateUtil
    {
        
        public static bool IsInPayPeriod(this DateTime date, DateTime startTime, DateTime endTime)
        {
            return (date >= startTime) && (date <= endTime);
        }
    }
}