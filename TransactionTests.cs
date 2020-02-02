using System.Runtime.CompilerServices;
using System.Reflection;
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
            AddEmployeeTransaction t = new AddSalariedEmployee(empId, "Bob", "Home", 1000);
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
        
        [Test]
        public void TestAddHourlyEmployee()
        {
            int empId = 1;
            AddEmployeeTransaction t = new AddHourlyEmployee(empId, "Bob", "Home", 12.75m);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e.Name, Is.EqualTo("Bob"));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is HourlyClassification, Is.True);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.That(hc.HourlyRate, Is.EqualTo(12.75m));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is WeeklySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
        }

                [Test]
        public void TestCommissionedEmployee()
        {
            int empId = 1;
            AddEmployeeTransaction t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000.00m, 0.1m);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e.Name, Is.EqualTo("Bob"));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is CommissionedClassification, Is.True);
            CommissionedClassification cc = pc as CommissionedClassification;
            Assert.That(cc.Salary, Is.EqualTo(1000.00m));
            Assert.That(cc.CommissionRate, Is.EqualTo(0.1m));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is BiWeeklySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
        }

    }
}