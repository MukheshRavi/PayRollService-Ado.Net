using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Payroll_Service_Ado
{
    public class PayrollRepository
    {
        public static string connectionString = @"Server=MUKESH\SQLEXPRESS; Initial Catalog =payroll_service;;Integrated Security=True;Connect Timeout=30;
Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);

        public bool CheckConnection()
        {
           
            try
            {
            // If no error return true else false
            connection.Open();
            connection.Close();
            
            return true;
            }
            catch
            { 
                return false;
            }
        }
        public void GetPayrollDetails()
        {
            PayrollDetails model = new PayrollDetails();
            try
            {
                using (connection)
                {
                    string query = @"select * from dbo.PayrollDetails";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Payrollid = reader.GetInt32(0);
                            model.BasePay = reader.GetInt32(1);
                            model.Deductions = reader.GetInt32(2);
                            model.TaxablePay = reader.GetInt32(3);
                            model.IncomeTax = reader.GetInt32(4);
                            Console.WriteLine("Payrollid" +" "+ "Basepay" +" "+ "Deductions" +" "+ "TaxablePay" +" "+ "IncomeTax" +" "+ "NetPay");
                            Console.WriteLine(model.Payrollid+" "+ model.BasePay + " "+ model.Deductions + " "+ model.TaxablePay + " "+ model.IncomeTax
                                +" "+(model.BasePay-model.Deductions-model.TaxablePay));
                        }
                    }
                    else
                        Console.WriteLine("No data found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateSalary()
        {
            PayrollDetails payroll = new PayrollDetails();
            int empId;
            try
            {
                using (connection)
                {
                    string query = @"select PayrollId from dbo.Employee where EmpName='Mukhesh'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            empId = reader.GetInt32(0);
                            
                        }
                    }
                    else
                        Console.WriteLine("No data found");
                    connection.Close();
                    string query1 = @"update  PayrollDetails set Basepay=" + 300000 + "where payrollId="+1+"";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    connection.Open();
                    command1.ExecuteNonQuery();
                    
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
                    
            
        }
    }
}
