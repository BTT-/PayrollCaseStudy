namespace payrollCaseStudy
{
    public class HourlyClassification : PaymentClassification
    {
        public decimal HourlyRate {get; set;}
        public HourlyClassification(decimal rate)
        {
            HourlyRate = rate;
        }
        
    }
}