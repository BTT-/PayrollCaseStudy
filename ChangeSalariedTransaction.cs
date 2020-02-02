namespace payrollCaseStudy
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly decimal amount;

        public ChangeSalariedTransaction(int empId, decimal amount) : base(empId)
        {
            this.amount = amount;
        }

        protected override PaymentSchedule Schedule{
            get { return new MonthlySchedule(); }
        }
        protected override PaymentClassification Classification {
            get { return new SalariedClassification(amount); }
        }
    }
}