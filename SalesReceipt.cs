using System;
namespace payrollCaseStudy
{
    public class SalesReceipt
    {
        private readonly decimal amount;
        private readonly DateTime date;

        public decimal Amount {
            get { return amount; }
        }
        public DateTime Date {
            get { return date; }
        }

        public SalesReceipt(DateTime date, decimal amount)
        {
            this.date = date;
            this.amount = amount;
        }
    }
}