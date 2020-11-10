CREATE PROCEDURE [dbo].[DeleteEmployee]
	@Empid int = null
AS
begin
begin try
update Employee set ISActive = 0 where EmpID = @Empid;
return 1;
end try
begin catch
return 0
end catch
end
