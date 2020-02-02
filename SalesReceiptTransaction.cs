using System;
namespace payrollCaseStudy
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly int empId;
        private readonly DateTime date;
        private readonly decimal amount;

        public SalesReceiptTransaction(DateTime date, decimal amount, int empId)
        {
            this.empId = empId;
            this.date = date;
            this.amount = amount;
        }

        public void Execute()
        {
            var e = PayrollDatabase.GetEmployee(empId);
            var cc = e?.Classification as CommissionedClassification;
            cc?.AddSalesReceipt(new SalesReceipt(date, amount));
        }

    }
}