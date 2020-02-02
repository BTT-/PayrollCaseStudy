namespace payrollCaseStudy
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly decimal salary;
        private readonly decimal rate;
        public ChangeCommissionedTransaction(int empId, decimal salary, decimal rate) : base(empId)
        {
            this.salary = salary;
            this.rate = rate;
        }

        protected override PaymentSchedule Schedule{
            get { return new BiWeeklySchedule(); }
        }
        protected override PaymentClassification Classification{
            get { return new CommissionedClassification(salary, rate); }
        }

    }
}