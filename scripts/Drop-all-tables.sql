--清空数据库内所有的表 --注意替换数据库
USE NetAdmin_Tsk 
GO
DECLARE
	@SQL VARCHAR ( 8000 )
WHILE
		( SELECT COUNT ( * ) FROM sysobjects WHERE type = 'U' ) > 0 BEGIN
		SELECT
			@SQL = 'drop table ' + name 
		FROM
			sysobjects 
		WHERE
			( type = 'U' ) 
		ORDER BY
		'drop table ' + name EXEC ( @SQL ) 
END


--清空数据库内所有的表 --注意替换数据库
USE NetAdmin_Sys
GO
DECLARE
	@SQL VARCHAR ( 8000 )
WHILE
		( SELECT COUNT ( * ) FROM sysobjects WHERE type = 'U' ) > 0 BEGIN
		SELECT
			@SQL = 'drop table ' + name 
		FROM
			sysobjects 
		WHERE
			( type = 'U' ) 
		ORDER BY
		'drop table ' + name EXEC ( @SQL ) 
END


--清空数据库内所有的表 --注意替换数据库
USE NetAdmin_Res
GO
DECLARE
	@SQL VARCHAR ( 8000 )
WHILE
		( SELECT COUNT ( * ) FROM sysobjects WHERE type = 'U' ) > 0 BEGIN
		SELECT
			@SQL = 'drop table ' + name 
		FROM
			sysobjects 
		WHERE
			( type = 'U' ) 
		ORDER BY
		'drop table ' + name EXEC ( @SQL ) 
END



--清空数据库内所有的表 --注意替换数据库
USE NetAdmin_Cst
GO
DECLARE
	@SQL VARCHAR ( 8000 )
WHILE
		( SELECT COUNT ( * ) FROM sysobjects WHERE type = 'U' ) > 0 BEGIN
		SELECT
			@SQL = 'drop table ' + name 
		FROM
			sysobjects 
		WHERE
			( type = 'U' ) 
		ORDER BY
		'drop table ' + name EXEC ( @SQL ) 
END