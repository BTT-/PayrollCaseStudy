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
                if(tc.Date.IsInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate))
                {
                    result += CalculatePayForTimeCard(tc);
                }
            }

            return result;
        }

        private decimal CalculatePayForTimeCard(TimeCard tc)
        {
            decimal overtimeHours = CalculateOvertimeHours(tc);
            decimal regularHours = CalculateRegularHours(tc, overtimeHours);
            return (regularHours + 1.5m * overtimeHours) * HourlyRate;
        }

        private decimal CalculateOvertimeHours(TimeCard tc)
        {
            return Convert.ToDecimal(Math.Max(0, tc.Hours-8));
        }

        private decimal CalculateRegularHours(TimeCard tc, decimal overtime)
        {
            return Convert.ToDecimal(tc.Hours) - overtime;
        }
    }
}