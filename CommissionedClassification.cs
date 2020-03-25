using System.Collections;
using System;
namespace payrollCaseStudy
{
    public class CommissionedClassification : PaymentClassification
    {
        public decimal Salary {get; set;}
        public decimal CommissionRate {get; set;}

        private Hashtable salesReceipts;

        public CommissionedClassification(decimal salary, decimal rate)
        {
            Salary = salary;
            CommissionRate = rate;
            salesReceipts = new Hashtable();
        }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return salesReceipts[date] as SalesReceipt;
        }

        public void AddSalesReceipt(SalesReceipt receipt)
        {
            salesReceipts[receipt.Date] = receipt;
        }

        public override decimal CalculatePay(Paycheck paycheck)
        {
            var result = Salary;
            foreach(SalesReceipt sr in salesReceipts.Values)
            {
                if(IsInPayPeriod(sr.Date, paycheck))
                {
                    result += CommissionRate * sr.Amount;
                }
            }
            return result;
        }
    }
}