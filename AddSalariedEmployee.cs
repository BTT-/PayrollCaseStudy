namespace payrollCaseStudy
{
    public class AddSalariedEmployee : Transaction
    {
        int EmpId {get; set;}
        string Name {get; set;}
        string Address {get; set;}
        PaymentClassification Classification {get; set;}
        PaymentSchedule Schedule {get; set;}
        PaymentMethod Method {get; set;}
        public AddSalariedEmployee(int empId, string name, string address, decimal salary)
        {
            EmpId = empId;
            Name = name;
            Address = address;
        }

        public void Execute(){

        }


    }
}