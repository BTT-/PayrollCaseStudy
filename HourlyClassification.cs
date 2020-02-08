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
            return 0m;
        }

    }
}