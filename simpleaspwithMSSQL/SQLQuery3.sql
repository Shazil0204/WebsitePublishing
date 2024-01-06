CREATE DATABASE UsersDB;
GO

USE UsersDB;
go

create table Users(
	userid int identity(1,1) primary key,
	username varchar(50) not null,
	age int not null
);
go

create procedure insertuser(
	@Username varchar(50),
	@age int
)
AS
BEGIN
	insert into Users(username, age) values(@Username, @age);
	
	select 1 as result;
END;
GO

USE master;
GO

-- Create a SQL Server login
CREATE LOGIN Userinfo
WITH PASSWORD = 'Userinfo', -- Change the password as needed
     CHECK_EXPIRATION = OFF,
     CHECK_POLICY = OFF;
GO

-- Create a database user and assign it to the 'owner' role in the UsersDB database
USE UsersDB; -- Change to your database name
GO

CREATE USER Userinfo FOR LOGIN Userinfo;
GO

ALTER ROLE owner ADD MEMBER Userinfo;
GO
