namespace payrollCaseStudy
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly decimal salary;
        private readonly decimal commissionRate;
        
        public AddCommissionedEmployee(int empId, string name, string address, decimal salary, decimal rate) : base(empId, name, address)
        {
            this.salary = salary;
            this.commissionRate = rate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new BiWeeklySchedule();
        }

        protected override PaymentClassification MakePaymentClassification()
        {
            return new CommissionedClassification(salary, commissionRate);
        }

    }
}