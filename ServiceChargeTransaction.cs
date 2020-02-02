using System;
namespace payrollCaseStudy
{
    public class ServiceChargeTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly decimal amount;

        private readonly int memberId;

        public ServiceChargeTransaction(int memberId, DateTime date, decimal amount)
        {
            this.date = date;
            this.amount = amount;
            this.memberId = memberId;
        }

        public void Execute()
        {
            var e = PayrollDatabase.GetUnionMember(memberId);
            var ua = e?.Affiliation as UnionAffiliation;
            ua?.AddServiceCharge(new ServiceCharge(date, amount));
        }

    }
}