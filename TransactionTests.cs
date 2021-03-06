using System.Reflection.Emit;
using System;
using NUnit.Framework;

namespace payrollCaseStudy
{
    [TestFixture]
    public class TransactionTests
    {
        [SetUp]
        public void Setup()
        {
            PayrollDatabase.InitializeDatabase();
        }

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

        [Test]
        public void TestChangeUnionMember()
        {
            int empId = 8;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Lance", "Home", 13.37m);
            t.Execute();
            int memberId = 7742;
            ChangeAffiliationTransaction cat = new ChangeMemberTransaction(empId, memberId, 99.42m);
            cat.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            Affiliation affiliation = e.Affiliation;
            Assert.That(affiliation, Is.Not.Null);
            Assert.That(affiliation is UnionAffiliation);
            UnionAffiliation ua = affiliation as UnionAffiliation;
            Assert.That(ua.Dues, Is.EqualTo(99.42m));
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.That(member, Is.Not.Null);
            Assert.That(e, Is.EqualTo(member));
        }

        [Test]
        public void TestChangeNoAffiliation()
        {
            int empId = 8;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Lance", "Home", 13.37m);
            t.Execute();
            int memberId = 7742;
            ChangeAffiliationTransaction cat = new ChangeMemberTransaction(empId, memberId, 99.42m);
            cat.Execute();
            ChangeAffiliationTransaction cuat = new ChangeUnaffiliatedTransaction(empId);
            cuat.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);
            Affiliation affiliation = e.Affiliation;
            Assert.That(affiliation, Is.Not.Null);
            Assert.That(affiliation is NoAffiliation);
            NoAffiliation na = affiliation as NoAffiliation;
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.That(member, Is.Null);
        }

        [Test]
        public void TestPaySingleSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m);
        }

        [Test]
        public void TestPaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 11, 29);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.That(pc, Is.Null);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 7);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 0.0m);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 7);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 30.5m);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 7);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 9.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, (8m + 1.5m) * 15.25m);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 6);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.That(pc, Is.Null);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 7);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();
            TimeCardTransaction tct2 = new TimeCardTransaction(payDate.AddDays(-5), 5.0, empId);
            tct2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 7m * 15.25m);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeWithTimeCardSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "home", 15.25m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 2, 7);
            DateTime dateInPreviousPeriod = payDate.AddDays(-7);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();
            TimeCardTransaction tct2 = new TimeCardTransaction(dateInPreviousPeriod, 10.0, empId);
            tct2.Execute();
            
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 30.5m);            
        }

        [Test]
        public void TestPaySingleCommissionedEmployeeNoReceipts()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 21);

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m);
        }

        [Test]
        public void TestPaySingleCommissionedEmployeeWrongDate()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 20);

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            var pc = pt.GetPaycheck(empId);
            Assert.That(pc, Is.Null);
        }

        [Test]
        public void TestPaySingleCommissionedEmployeeOneReceipts()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 0.5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 21);
            SalesReceiptTransaction srt = new SalesReceiptTransaction(payDate.AddDays(-1), 100m, empId);
            srt.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m + 0.5m * 100m);
        }

        [Test]
        public void TestPaySingleCommissionedEmployeeTwoReceipts()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 0.5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 21);
            SalesReceiptTransaction srt = new SalesReceiptTransaction(payDate, 100m, empId);
            srt.Execute();
            SalesReceiptTransaction srt2 = new SalesReceiptTransaction(payDate.AddDays(-13), 100m, empId);
            srt2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m + 0.5m * 200m);
        }

        [Test]
        public void TestPaySingleCommissionedEmployeeTwoPeriods()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 0.5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 21);
            SalesReceiptTransaction srt = new SalesReceiptTransaction(payDate, 100m, empId);
            srt.Execute();
            SalesReceiptTransaction srt2 = new SalesReceiptTransaction(payDate.AddDays(-13), 100m, empId);
            srt2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m + 0.5m * 200m);


            payDate = new DateTime(2020, 03, 06);
            pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m);

        }        

        [Test]
        public void TestPaySingleCommissionedEmployeeSalesReceiptSpanningTwoPeriods()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000m, 0.5m);
            t.Execute();
            DateTime payDate = new DateTime(2020, 02, 21);
            SalesReceiptTransaction srt = new SalesReceiptTransaction(payDate.AddDays(-1), 100m, empId);
            srt.Execute();
            SalesReceiptTransaction srt2 = new SalesReceiptTransaction(payDate.AddDays(-15), 100m, empId);
            srt2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m + 0.5m * 100m);
        } 

        [Test]
        public void TestSalariedUnionMemberDues()
        {
            int empId = 1;
            var t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00m);
            t.Execute();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();

            DateTime payDate = new DateTime(2020, 02, 29);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            //4 Fridays in Feb 2020
            var unionDues = 4m*9.42m;
            ValidatePaycheck(pt, empId, payDate, 1000m, unionDues);
        }

        [Test]
        public void TestSalariedUnionMemberDuesFridaysTest()
        {
            int empId = 1;
            var t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00m);
            t.Execute();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();

            DateTime payDate = new DateTime(2020, 01, 31);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            //5 Fridays in Jan
            var unionDues = 5m*9.42m;
            ValidatePaycheck(pt, empId, payDate, 1000m, unionDues);

        }

        [Test]
        public void TestCommissionedUnionMemberDues()
        {
            int empId = 1;
            var t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000.00m, 1m);
            t.Execute();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();

            DateTime payDate = new DateTime(2020, 01, 24);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();
            //2 Fridays 
            var unionDues = 2m*9.42m;

            ValidatePaycheck(pt, empId, payDate, 1000m, unionDues);

            payDate = new DateTime(2020, 01, 10);
            pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidatePaycheck(pt, empId, payDate, 1000m, unionDues);

        }

        [Test]
        public void TestHourlyUnionMemberDues()
        {
            int empId = 1;
            var t = new AddHourlyEmployee(empId, "Bob", "Home", 12.0m);
            t.Execute();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();

            DateTime payDate = new DateTime(2020, 01, 24);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            //1 Friday
            var unionDues = 9.42m;
            ValidatePaycheck(pt, empId, payDate, 0m, unionDues);

        }

        [Test]
        public void TestHourlyUnionMemberServiceCharge()
        {
            int empId = 12;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24m);
            t.Execute();

            int memberId = 1337;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();
            DateTime payDate = new DateTime(2020, 03, 20);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42m);
            sct.Execute();
            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empId);
            tct.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);

            ValidatePaycheck(pt, empId, payDate, 8*15.24m, 9.42m+19.42m);
        }

        [Test]
        public void TestServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = 1;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.23m);
            t.Execute();

            int memberId = 1234;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42m);
            cmt.Execute();

            var payDate = new DateTime(2020,03,20);
            var earlyDate = new DateTime(2020, 03, 13);
            var lateDate = new DateTime(2020, 03, 27);

            var sct = new ServiceChargeTransaction(memberId, payDate, 19.42m);
            sct.Execute();
            var sctEarly = new ServiceChargeTransaction(memberId, earlyDate, 100.00m);
            sctEarly.Execute();
            var sctLate = new ServiceChargeTransaction(memberId, lateDate, 100.00m);
            sctLate.Execute();
            var tct = new TimeCardTransaction(payDate, 8.0, empId);
            tct.Execute();
            var pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 8 * 15.23m, 9.42m+19.42m);

        }

        private void ValidatePaycheck(PaydayTransaction pt, int empId, DateTime payDate, decimal pay, decimal deductions = 0m)
        {
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.That(pc, Is.Not.Null);
            Assert.That(pc.PayPeriodEndDate, Is.EqualTo(payDate));
            Assert.That(pc.GrossPay, Is.EqualTo(pay));
            Assert.That(pc.Deductions, Is.EqualTo(deductions));
            Assert.That(pc.NetPay, Is.EqualTo(pay - deductions));
        }
    }
}