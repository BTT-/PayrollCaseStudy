namespace payrollCaseStudy
{
    public class SalariedClassification : PaymentClassification
    {
        public decimal Salary {get; set;}

        public SalariedClassification(decimal salary)
        {
            Salary = salary;
        }

        public override decimal CalculatePay(Paycheck paycheck)
        {
            return Salary;
        }

    }
}