namespace payrollCaseStudy
{
    public class SalariedClassification : PaymentClassification
    {
        public decimal Salary {get; set;}

        public SalariedClassification(decimal salary)
        {
            Salary = salary;
        }

        public decimal CalculatePay(Paycheck paycheck)
        {
            return Salary;
        }

    }
}