using System;

namespace Payroll_Service_Ado
{
    class Program
    {
        static void Main(string[] args)
        {
            PayrollRepository payrollRepository = new PayrollRepository();
            Console.WriteLine("Welcome to Payroll Service");
           // payrollRepository.GetPayrollDetails();
           // payrollRepository.UpdateSalary();
            payrollRepository.RetrieveWithStartDate();

        }
    }
}
