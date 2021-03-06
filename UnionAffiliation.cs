using System.Diagnostics;
using System.Collections;
using System;
namespace payrollCaseStudy
{
    public class UnionAffiliation : Affiliation
    {

        private Hashtable servicecharges;
        private decimal dues;

        private readonly int memberId;

        public int MemberId{
            get { return memberId; }
        }
        public decimal Dues{
            get { return dues; }
        }

        public UnionAffiliation()
        {
            servicecharges = new Hashtable();
        }

        public UnionAffiliation(int memberId, decimal dues) : this()
        {
            this.dues = dues;
            this.memberId = memberId;
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            servicecharges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return servicecharges[date] as ServiceCharge;
        }

        public decimal CalculateDeductions(Paycheck paycheck)
        {
            decimal result = CalculateBaseDues(paycheck);
            result += CalculateServiceChargeAmount(paycheck);

            return result;
        }

        private decimal CalculateBaseDues(Paycheck paycheck)
        {
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);
            return fridays * dues;
        }

        private decimal CalculateServiceChargeAmount(Paycheck paycheck)
        {
            var result = 0m;
            foreach(ServiceCharge charge in servicecharges.Values)
            {
                if(charge.Date.IsInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate))
                {
                    result += charge.Amount;
                }
            }
            return result;
        }

        private int NumberOfFridaysInPayPeriod(DateTime startDate, DateTime endDate)
        {
            int result = 0;
            for(DateTime day = startDate; day <= endDate; day = day.AddDays(1))
            {
                if(day.DayOfWeek == DayOfWeek.Friday) 
                    result++;
            }
            return result;
        }
    }
}