namespace payrollCaseStudy
{
    public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empId) : base(empId)
        {
            
        }

        protected override Affiliation Affiliation{
            get { return new NoAffiliation(); }
        }

        protected override void RecordMembership(Employee e)
        {
            if(e.Affiliation is UnionAffiliation ua)
            {
                var memberId = ua.MemberId;
                PayrollDatabase.RemoveUnionMember(memberId);
            }
        }


    }
}