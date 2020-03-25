using System.Collections;
using System;

namespace payrollCaseStudy
{
    public class PaydayTransaction : Transaction
    {
        
        private readonly DateTime payDate;
        private Hashtable paychecks;
        public PaydayTransaction(DateTime payDate)
        {
            paychecks = new Hashtable();
            this.payDate = payDate;
        }

        public void Execute()
        {
            var empIds = PayrollDatabase.GetEmployeeIds();
            foreach(int empId in empIds)
            {
                var e = PayrollDatabase.GetEmployee(empId);
                if(e.IsPayDay(payDate))
                {
                    var startDate = e.GetPayPeriodStartDate(payDate);
                    Paycheck pc = new Paycheck(startDate, payDate);
                    paychecks[empId] = pc;
                    e.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId)
        {
            return paychecks[empId] as Paycheck;
        }
    }
}