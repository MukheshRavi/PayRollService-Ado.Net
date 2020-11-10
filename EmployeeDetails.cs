using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll_Service_Ado
{
    public class EmployeeDetails
    {
        public int empId { get; set; }
        public string empName { get; set; }
        public char gender { get; set; }
        public string phoneNumber { get; set; }
        public int payrollId { get; set; }
        public DateTime startDate { get; set; }
    }
}
