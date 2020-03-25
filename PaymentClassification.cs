using System;

namespace payrollCaseStudy
{
    public abstract class PaymentClassification
    {
        public abstract decimal CalculatePay(Paycheck paycheck);

        public bool IsInPayPeriod(DateTime date, Paycheck paycheck)
        {
            var payPeriodEndDate = paycheck.PayPeriodEndDate;
            var payPeriodStartDate = paycheck.PayPeriodStartDate;

            return (date >= payPeriodStartDate) && (date <= payPeriodEndDate);
        }

    }
}