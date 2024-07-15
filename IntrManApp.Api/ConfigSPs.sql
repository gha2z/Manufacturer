SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Config]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Config](
	[Name] [nvarchar](50) NULL,
	[Value] [nvarchar](500) NULL,
	[Context] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BackupDB]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BackupDB] AS' 
END
GO
ALTER PROC [dbo].[BackupDB] 
(@DB nvarchar(100))  
AS 
Begin Try 
	declare @Path varchar(max) 
	declare @Fn varchar(100) 
	declare @AppDate int 
	select @Path=Value from dbo.Config where Name='BackupFolder' and Context=@DB 
	select @Fn=Value from dbo.Config where Name='BackupFileName' and Context=@DB 
	select @AppDate=Value from dbo.Config where Name='BackupAppendDateToFileName' and Context=@DB 

	if @AppDate<>0 
		set @Fn=replace(replace(@Fn+convert(varchar(30), getdate(),13)+'.bak',' ','_'),':','_') 
	
	if right(@Path,1)<>'\' set @Path=@Path+'\' 
		set @Path=@Path+@Fn 
	
	declare @login nvarchar(200)=''
	if charIndex('\',@@SERVERNAME,1)>0 
		SET @login=substring(@@servername,1,charindex('\',@@servername)-1)+'\Administrator' 
	else
		SET @login=@@SERVERNAME+'\Administrator'

	
	execute as login=@login	
	Backup database @Db to Disk=@Path with format 
	revert 

	select @Path as Path, cast (1 as bit) as IsSucceeded, '' as ErrorMessage
end try 
Begin Catch 
   DECLARE @ErrorMessage NVARCHAR(4000);
   DECLARE @ErrorSeverity INT;
   DECLARE @ErrorState INT;
   SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(),    @ErrorState = ERROR_STATE(); 
   --RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); 
	select  @Path as Path, cast (0 as bit) as Succeeded, @ErrorMessage as ErrorMessage
end catch

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestoreDB]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RestoreDB] AS' 
END
GO
ALTER PROC [dbo].[RestoreDB] 
(
	@Db nvarchar(100),
	@Path nvarchar(max))  
AS 
Begin Try 
	declare @login nvarchar(100)
	if charIndex('\',@@SERVERNAME,1)>0 
		SET @login=substring(@@servername,1,charindex('\',@@servername)-1)+'\Administrator' 
	else
		SET @login=@@SERVERNAME+'\Administrator'

	DECLARE @kill varchar(8000) = '';  
	SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
	FROM master.sys.dm_exec_sessions
	WHERE database_id  = db_id(@DB)

	
	EXEC(@kill);
	
	execute as login=@login	
	RESTORE DATABASE @Db FROM DISK = @Path
	WITH REPLACE
	revert 

	select @Path as Path, cast (1 as bit) as IsSucceeded, '' as ErrorMessage
end try 
Begin Catch 
   DECLARE @ErrorMessage NVARCHAR(4000);
   DECLARE @ErrorSeverity INT;
   DECLARE @ErrorState INT;
   SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(),    @ErrorState = ERROR_STATE(); 
   --RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); 
	select  @Path as Path, cast (0 as bit) as Succeeded, @ErrorMessage as ErrorMessage
end catch
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetBackupDiskPath]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SetBackupDiskPath] AS' 
END
GO
ALTER Proc SetBackupDiskPath
(
	@Db nvarchar(50),
	@Path NVarchar(250),
	@FileName nvarchar(50),
	@AppendDateTime bit
)
as
DELETE from dbo.Config WHERE Name='BackupFolder' and Context=@Db 
INSERT dbo.Config values ('BackupFolder',@Path,@Db)
DELETE from dbo.Config WHERE Name='BackupFileName' and Context=@Db 
INSERT dbo.Config values ('BackupFileName',@FileName,@Db)
DELETE from dbo.Config WHERE Name='BackupAppendDateToFileName' and Context=@Db 
INSERT dbo.Config values ('BackupAppendDateToFileName',@AppendDateTime,@Db)
GO
