CREATE PROCEDURE [dbo].[AddNewEmployee]
	@EmpName varchar(50),
	@Gender varchar(1),
	@PhoneNumber varchar(10),
	@start_date datetime,
	@BasePay int,
	@Deductions int,
	@IncomeTax int,
	@TaxablePay int 
as
begin
begin try
begin transaction
Declare @EmpId int = null
Declare @PayrollId int = null
begin
insert into PayrollDetails values (@BasePay,@Deductions,@IncomeTax,@TaxablePay);
end
	begin
	select @PayrollId=count(*) from PayrollDetails;
	end
	begin
	insert into Employee values 
	(@EmpName,@Gender,@PhoneNumber,@start_date,@PayrollId);
	end
	begin
    select @EmpId= count(*) from Employee;
	end
	begin
    insert into Employee_Department  values (@Empid,1);
	end
commit transaction;
return 0;
end try
begin catch
rollback transaction;
end catch
end
return -1