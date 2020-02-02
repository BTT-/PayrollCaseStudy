namespace payrollCaseStudy
{
    public class CommissionedClassification : PaymentClassification
    {
        public decimal Salary {get; set;}
        public decimal CommissionRate {get; set;}

        public CommissionedClassification(decimal salary, decimal rate)
        {
            Salary = salary;
            CommissionRate = rate;
        }

    }
}