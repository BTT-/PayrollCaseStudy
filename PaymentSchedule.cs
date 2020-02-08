using System;

namespace payrollCaseStudy
{
    public interface PaymentSchedule
    {
        bool IsPayDay(DateTime payDate);
    }
}