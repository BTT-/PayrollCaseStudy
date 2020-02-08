namespace payrollCaseStudy
{
    public class NoAffiliation : Affiliation
    {
        public decimal CalculateDeductions(Paycheck paycheck)
        {
            return 0m;
        }   
    }
}