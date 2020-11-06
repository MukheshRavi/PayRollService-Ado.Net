using NUnit.Framework;
using Payroll_Service_Ado;

namespace TestEmployeePayRoll
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            PayrollRepository payrollRepository = new PayrollRepository();
            bool expected = true;
            bool actual=payrollRepository.CheckConnection();
            Assert.AreEqual(expected, actual);
        }
    }
}