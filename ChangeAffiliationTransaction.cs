namespace payrollCaseStudy
{
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {

        public ChangeAffiliationTransaction(int empId) : base(empId)
        {
            
        }
        
        protected override void Change(Employee e)
        {
            RecordMembership(e);
            e.Affiliation = Affiliation;
        }

        protected abstract void RecordMembership(Employee e);

        protected abstract Affiliation Affiliation {
            get;
        }

    }
}