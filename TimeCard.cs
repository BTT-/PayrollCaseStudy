using System;
namespace payrollCaseStudy
{
    public class TimeCard
    {
        private readonly double hours;
        private readonly DateTime date;
        public double Hours {get {
            return hours;
        }
        }

        public DateTime Date {
            get{
                return date;
            }
        }

        public TimeCard(DateTime date, double hours)
        {
            this.date = date;
            this.hours = hours;
        }
    }
}