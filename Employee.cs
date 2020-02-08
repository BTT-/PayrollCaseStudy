using System;

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
        public Affiliation Affiliation { get; set; }

        public Employee(int empId, string name, string address)
        {
            EmpId = empId;
            Name = name;
            Address = address;
            Affiliation = new NoAffiliation();
        }

        public void Payday(Paycheck paycheck)
        {
            decimal grossPay = Classification.CalculatePay(paycheck);
            decimal deductions = Affiliation.CalculateDeductions(paycheck);
            decimal netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            Method.Pay(paycheck);
        }

        public bool IsPayDay(DateTime payDate)
        {
            return Schedule.IsPayDay(payDate);
        }


    }
}