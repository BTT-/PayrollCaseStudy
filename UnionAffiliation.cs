using System.Collections;
using System;
namespace payrollCaseStudy
{
    public class UnionAffiliation : Affiliation
    {

        private Hashtable servicecharges;
        private decimal dues;

        private readonly int memberId;

        public decimal Dues{
            get { return dues; }
        }

        public UnionAffiliation()
        {
            servicecharges = new Hashtable();
        }

        public UnionAffiliation(int memberId, decimal dues) : this()
        {
            this.dues = dues;
            this.memberId = memberId;
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            servicecharges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return servicecharges[date] as ServiceCharge;
        }
    }
}