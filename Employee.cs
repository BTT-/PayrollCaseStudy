namespace payrollCaseStudy
{
    public class Employee
    {
        public int EmpId {get; set;}
        public string Name {get; set;}
        public string Address {get; set;}
        public PaymentClassification Classification {get; set;}
        public PaymentSchedule Schedule {get; set;}
        public PaymentMethod Method {get; set;}

        

    }
}