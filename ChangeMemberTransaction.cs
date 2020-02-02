namespace payrollCaseStudy
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        private readonly int memberId;
        private readonly decimal dues;
        public ChangeMemberTransaction(int empId, int memberId, decimal dues) : base(empId)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        protected override void RecordMembership(Employee e)
        {
            PayrollDatabase.AddUnionMember(memberId, e.EmpId);
        }

        protected override Affiliation Affiliation{
            get { return new UnionAffiliation(memberId, dues); }
        }

    }
}