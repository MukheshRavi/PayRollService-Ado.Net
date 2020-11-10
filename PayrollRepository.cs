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

        /// <summary>
        /// This method establishes connection and checks the connection
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This method retrieves data present in PayrollDetails 
        /// </summary>
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
        /// <summary>
        /// Method updates salary of a particular employee
        /// </summary>
        /// <returns></returns>
        public int UpdateSalary()
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
                   int result= command1.ExecuteNonQuery();
                    return result;
                    
                    
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
        public void RetrieveWithStartDate()
        {
            try
            {
                // Give the query
                string query= @"select * from Employee e where start_date between cast('2020-01-01' as date) and cast(getdate() as date) where e.IsActive=1";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                EmployeeDetails employeeDetails = new EmployeeDetails();
                while (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        employeeDetails.empId = Convert.ToInt32(reader[0]);
                        employeeDetails.empName = Convert.ToString(reader[1]);
                        employeeDetails.gender = Convert.ToChar(reader[2]);
                        employeeDetails.phoneNumber = Convert.ToString(reader[3]);
                        employeeDetails.payrollId = Convert.ToInt32(reader[5]);
                        employeeDetails.startDate = Convert.ToDateTime(reader[4]);
                        Console.WriteLine(employeeDetails.empId + "       " + employeeDetails.empName + "        " +
                                           employeeDetails.gender + "         " + employeeDetails.phoneNumber + "         " +
                                           employeeDetails.payrollId + "          " + employeeDetails.startDate);
                       
                    }
                    else
                        break;
                }
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                    connection.Close();
            }
        }
        public void ImplementDatabaseFunctions()
        {
            try
            {
                using (this.connection)
                {
                    string query = @"select e.Gender ,min(p.BasePay-p.Deductions-p.TaxablePay)as MinNetPay,
                                                      max(p.BasePay-p.Deductions-p.TaxablePay)as MaxNetPay,
                                                      sum(p.BasePay-p.Deductions-p.TaxablePay)as SumNetPay,
                                                      Avg(p.BasePay-p.Deductions-p.TaxablePay)as AvgNetPay,count(e.gender) as Count
                                   from PayrollDetails p inner join Employee e on e.PayrollId=p.payrollId group by e.Gender";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string gender = reader[0].ToString();
                            double minNetPay = reader.GetInt32(1);
                            double maxNetPay= reader.GetInt32(2);
                            double sumNetPay = reader.GetInt32(3);
                            double avgNetPay = reader.GetInt32(4);
                            int count = reader.GetInt32(5);
                            Console.WriteLine("Gender:{0}\tCount:{1}\tMinSalary:{2}\tMaxSalary:{3}\tSumOfSalary:{4}\tAvgSalary:{5}\n", gender, count, minNetPay, 
                                maxNetPay, sumNetPay, avgNetPay);
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
        public bool AddEmployee(EmployeeDetails emp, PayrollDetails payrollDetails)
        {
            // open connection and create transaction
            connection.Open();
            try
            {
                // create a new command in transaction
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Execute command
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddNewEmployee";
                command.Parameters.AddWithValue("@EmpName", emp.empName);
                command.Parameters.AddWithValue("@gender", emp.gender);
                command.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                command.Parameters.AddWithValue("@start_date", emp.startDate);
                command.Parameters.AddWithValue("@BasePay", payrollDetails.BasePay);
                command.Parameters.AddWithValue("@Deductions", payrollDetails.Deductions);
                command.Parameters.AddWithValue("@Incometax", payrollDetails.IncomeTax);
                command.Parameters.AddWithValue("@TaxablePay", payrollDetails.TaxablePay);


                var result = command.ExecuteNonQuery();
                if (result != 1)
                    return false;
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                    connection.Close();
            }
        }
        /// <summary>
        /// UC 12 set _active and delete employee
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int empid)
        {
            // open connection and create transaction
            connection.Open();
            try
            {
                // create a new command in transaction
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Execute command
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.DeleteEmployee";
                command.Parameters.AddWithValue("@Empid", empid);

                var result = command.ExecuteNonQuery();
                if (result != 1)
                    return false;
                return true;
            }
            catch(Exception ex)
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
