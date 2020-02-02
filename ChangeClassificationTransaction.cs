namespace payrollCaseStudy
{
    public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        public ChangeClassificationTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Classification = Classification;
            e.Schedule = Schedule;
        }

        protected abstract PaymentSchedule Schedule { get; }
        protected abstract PaymentClassification Classification { get; }

    }
}