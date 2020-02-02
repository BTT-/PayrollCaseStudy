using System.Collections;
namespace payrollCaseStudy
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        private static Hashtable unionmembers = new Hashtable();
        public static void AddEmployee(int id, Employee employee)
        {
            employees[id] = employee;
        }

        public static Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }

        public static void DeleteEmployee(int id)
        {
            employees[id] = null;
        }

        public static void AddUnionMember(int memberId, int empId)
        {
            unionmembers[memberId] = empId;
        }

        public static Employee GetUnionMember(int memberId)
        {
            if(unionmembers[memberId] != null)
            {
                return employees[unionmembers[memberId]] as Employee;
            }
            return null;
        }

    }
}