using System;
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
            int empId = 2;
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
            int empId = 3;
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

        [Test]
        public void TestDeleteEmployee()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500m, 3.2m);
            t.Execute();

            Employee e  = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);

            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId);
            dt.Execute();

            e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Null);

        }

        [Test]
        public void TestTimeCardTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25m);
            t.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2020, 02, 02), 8.0, empId);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);

            PaymentClassification pc = e.Classification;
            Assert.That(pc is HourlyClassification, Is.True);
            HourlyClassification hc = pc as HourlyClassification;
            TimeCard tc = hc.GetTimeCard(new DateTime(2020, 02, 02));
            Assert.That(tc, Is.Not.Null);
            Assert.That(tc.Hours, Is.EqualTo(8.0));
        }

        [Test]
        public void TestSalesReceiptTransaction()
        {
            int empId = 6;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500m, 3.5m);
            t.Execute();

            SalesReceiptTransaction srt = new SalesReceiptTransaction(new DateTime(2020, 02, 02), 500000m, empId);
            srt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);

            PaymentClassification pc = e.Classification;
            Assert.That(pc is CommissionedClassification, Is.True);
            CommissionedClassification cc = pc as CommissionedClassification;

            SalesReceipt sr = cc.GetSalesReceipt(new DateTime(2020, 02, 02));
            Assert.That(sr, Is.Not.Null);
            Assert.That(sr.Amount, Is.EqualTo(500000m));
        }

        [Test]
        public void TestAddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.75m);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);

            UnionAffiliation ua = new UnionAffiliation();
            e.Affiliation = ua;
            int memberId = 86;
            PayrollDatabase.AddUnionMember(memberId, empId);

            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, new DateTime(2020,02,02), 12.95m);
            sct.Execute();
            ServiceCharge sc = ua.GetServiceCharge(new DateTime(2020,02,02));
            Assert.That(sc, Is.Not.Null);
            Assert.That(sc.Amount, Is.EqualTo(12.95m));
        }

        [Test]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 12.99m);
            t.Execute();
            ChangeEmployeeTransaction cnt = new ChangeNameTransaction(empId, "Bill");
            cnt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            Assert.That(e.Name, Is.EqualTo("Bill"));
        }

        [Test]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 2500m, 3.4m);
            t.Execute();

            ChangeClassificationTransaction cht = new ChangeHourlyTransaction(empId, 27.52m);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            PaymentClassification pc = e.Classification;
            Assert.That(pc, Is.Not.Null);
            Assert.That(pc is HourlyClassification, Is.True);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.That(hc.HourlyRate, Is.EqualTo(27.52m));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is WeeklySchedule);
        }

        [Test]
        public void TestChangeSalariedTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 2500m, 3.4m);
            t.Execute();

            ChangeClassificationTransaction cht = new ChangeSalariedTransaction(empId, 3000m);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            PaymentClassification pc = e.Classification;
            Assert.That(pc, Is.Not.Null);
            Assert.That(pc is SalariedClassification, Is.True);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.That(sc.Salary, Is.EqualTo(3000m));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is MonthlySchedule);
        }

        [Test]
        public void TestChangeCommissionedTransaction()
        {
            int empId = 3;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 3000m);
            t.Execute();

            ChangeClassificationTransaction cht = new ChangeCommissionedTransaction(empId, 2500m, 3.4m);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            PaymentClassification pc = e.Classification;
            Assert.That(pc, Is.Not.Null);
            Assert.That(pc is CommissionedClassification, Is.True);
            CommissionedClassification cc = pc as CommissionedClassification;
            Assert.That(cc.Salary, Is.EqualTo(2500m));
            Assert.That(cc.CommissionRate, Is.EqualTo(3.4m));
            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is BiWeeklySchedule);
        }

    }
}