namespace payrollCaseStudy
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly decimal hourlyRate;
        public AddHourlyEmployee(int empId, string name, string address, decimal rate) :base(empId, name, address)
        {
            hourlyRate = rate;
        }

        protected override PaymentSchedule MakePaymentSchedule()
        {
            return new WeeklySchedule();
        } 
        protected override PaymentClassification MakePaymentClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

    }
}