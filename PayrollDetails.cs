using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll_Service_Ado
{
    class PayrollDetails
    {
        public int Payrollid { get; set; }
        public int BasePay { get; set; }
        public int Deductions { get; set; }
        public int TaxablePay { get; set; }
        public int IncomeTax { get; set; }
    }
}
