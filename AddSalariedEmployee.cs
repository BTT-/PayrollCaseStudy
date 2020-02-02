namespace payrollCaseStudy
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly decimal salary;
        public AddSalariedEmployee(int empId, string name, string address, decimal salary) : base(empId, name, address)
        {
            this.salary = salary;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new MonthlySchedule();
        }
        protected override PaymentClassification MakePaymentClassification()
        {
            return new SalariedClassification(salary);
        }



    }
}