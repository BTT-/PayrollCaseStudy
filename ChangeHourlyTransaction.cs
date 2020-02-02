namespace payrollCaseStudy
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        private readonly decimal amount;

        public ChangeHourlyTransaction(int empId, decimal amount) : base(empId)
        {
            this.amount = amount;
        }

        protected override PaymentClassification Classification {
            get { return new HourlyClassification(amount); }
        }

        protected override PaymentSchedule Schedule
        {
            get { return new WeeklySchedule(); }
        }
        
    }
}