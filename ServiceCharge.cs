using System;
namespace payrollCaseStudy
{
    public class ServiceCharge
    {
        private readonly DateTime date;
        private readonly decimal amount;
        public DateTime Date 
        {
            get{ return date; }
        }
        public decimal Amount{
            get { return amount; }
        }

        public ServiceCharge(DateTime date, decimal amount)
        {
            this.date = date;
            this.amount = amount;
        }

    }
}