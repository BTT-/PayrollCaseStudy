namespace payrollCaseStudy
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        private readonly int empId;
        private readonly string name;
        private readonly string address;

        public AddEmployeeTransaction(int empId, string name, string address)
        {
            this.empId = empId;
            this.name = name;
            this.address = address;
        }

        public void Execute()
        {
            PaymentClassification pc = MakePaymentClassification();
            PaymentSchedule ps = MakePaymentSchedule();
            PaymentMethod pm = new HoldMethod();

            Employee e = new Employee(empId, name, address);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;
            PayrollDatabase.AddEmployee(empId, e);
        }

        protected abstract PaymentClassification MakePaymentClassification();
        protected abstract PaymentSchedule MakePaymentSchedule();


    }
}