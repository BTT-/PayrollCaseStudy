using System;

namespace payrollCaseStudy
{
    public class Paycheck
    {
        public decimal GrossPay {get; set;}
        public decimal Deductions {get; set;}
        public decimal NetPay {get; set;}
        public DateTime PayDate {get;}

        public Paycheck(DateTime payDate)
        {
            PayDate = payDate;
        }

    }
}