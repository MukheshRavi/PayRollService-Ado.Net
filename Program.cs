using System;

namespace Payroll_Service_Ado
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Payroll Service");
            new PayrollRepository().GetPayrollDetails();
            new PayrollRepository().UpdateSalary();
        }
    }
}
