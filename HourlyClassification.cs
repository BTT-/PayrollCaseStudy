using System.Collections;
using System.Collections.Generic;
using System;
namespace payrollCaseStudy
{
    public class HourlyClassification : PaymentClassification
    {
        private Hashtable timeCards;
        
        public decimal HourlyRate {get; set;}
        public HourlyClassification(decimal rate)
        {
            HourlyRate = rate;
            timeCards = new Hashtable();
        }
        
        public TimeCard GetTimeCard(DateTime date)
        { 
                return timeCards[date] as TimeCard;
        }

        public void AddTimeCard(TimeCard timeCard)
        {
            timeCards[timeCard.Date] = timeCard;
        }

        public decimal CalculatePay(Paycheck paycheck)
        {
            decimal result = 0m;
            foreach(TimeCard tc in timeCards.Values)
            {
                if(IsInPayPeriod(tc, paycheck.PayDate))
                {
                    result += Convert.ToDecimal(tc.Hours) * HourlyRate;
                }
            }

            return result;
        }

        private bool IsInPayPeriod(TimeCard tc, DateTime payDate)
        {
            DateTime payPeriodStart = payDate.AddDays(-5);
            DateTime payPeriodEnd = payDate;

            return tc.Date <= payPeriodEnd && tc.Date >= payPeriodStart;
        }
    }
}