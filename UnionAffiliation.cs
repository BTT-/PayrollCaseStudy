using System.Collections;
using System;
namespace payrollCaseStudy
{
    public class UnionAffiliation : Affiliation
    {

        private Hashtable servicecharges;

        public UnionAffiliation()
        {
            servicecharges = new Hashtable();
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