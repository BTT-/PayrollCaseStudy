using System.Transactions;
using System;
namespace payrollCaseStudy
{
    public class TimeCardTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double hours;
        private readonly int empId;
        public TimeCardTransaction(DateTime date, double hours, int empId)
        {
            this.empId = empId;
            this.hours = hours;
            this.date = date;
        }

        public void Execute()
        {
            var e = PayrollDatabase.GetEmployee(empId);
            var hc = e?.Classification as HourlyClassification;
            hc?.AddTimeCard(new TimeCard(date, hours));
        }

    }
}