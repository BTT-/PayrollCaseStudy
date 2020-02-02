namespace payrollCaseStudy
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empId;

        public DeleteEmployeeTransaction(int empId)
        {
            this.empId = empId;
        }

        public void Execute()
        {
            PayrollDatabase.DeleteEmployee(empId);
        }

    }
}