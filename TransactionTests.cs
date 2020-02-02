using NUnit.Framework;

namespace payrollCaseStudy
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e.Name, Is.EqualTo("Bob"));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is SalariedClassification, Is.True);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.That(sc.Salary, Is.EqualTo(1000));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is MonthlySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
            
        }
        
    }
}