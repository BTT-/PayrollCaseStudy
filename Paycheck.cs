using System;

namespace payrollCaseStudy
{
    public class Paycheck
    {
        public decimal GrossPay {get; set;}
        public decimal Deductions {get; set;}
        public decimal NetPay {get; set;}
        public DateTime PayPeriodEndDate {get;}
        public DateTime PayPeriodStartDate { get; internal set; }

        public Paycheck(DateTime startDate, DateTime endDate)
        {
            PayPeriodEndDate = endDate;
            PayPeriodStartDate = startDate;
        }

    }
}