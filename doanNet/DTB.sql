CREATE TABLE Faculty
(
  IDFalcuty INT NOT NULL,
  NameFalculty NVARCHAR(70) NOT NULL,
  Description NTEXT NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
  PRIMARY KEY (IDFalcuty)
);

CREATE TABLE Place
(
  IDPlace NVARCHAR(10) NOT NULL,
  Description NTEXT NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
  PRIMARY KEY (IDPlace)

);

CREATE TABLE Priority
(
  IDPriority INT NOT NULL,
  PriorityDescription INT NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
  PRIMARY KEY (IDPriority)
);

CREATE TABLE Room
(
  IDRoom INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Name NVARCHAR(50) NOT NULL,
  Floor INT NOT NULL,
  Building VARCHAR(10) NOT NULL,
  RoomType INT NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
);

CREATE TABLE Contract
(
  IDContract INT NOT NULL IDENTITY(1,1),
  MSSV NVARCHAR(30) NOT NULL,
  IDCitizen NVARCHAR(20) NOT NULL,
  ProfilePlace BINARY NOT NULL,
  IDPlace NVARCHAR(10) NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
  PRIMARY KEY (IDContract),
  FOREIGN KEY (IDPlace) REFERENCES Place(IDPlace)
);

CREATE TABLE ContractBridge
(
  IDContract INT NOT NULL,
  IDPriority INT NOT NULL,
  [Order] int,
  [Meta] text,
  DateBegin datetime NOT NULL,
  Hide int NOT NULL,
  PRIMARY KEY (IDContract, IDPriority),
  FOREIGN KEY (IDContract) REFERENCES Contract(IDContract),
  FOREIGN KEY (IDPriority) REFERENCES Priority(IDPriority)
);

CREATE TABLE SinhVien
(
  IDSinhVien INT NOT NULL IDENTITY(1,1),
  FullName NVARCHAR(50) NOT NULL,
  Address NTEXT NOT NULL,
  BirthDay DATE NOT NULL,
  AttendDate DATE NOT NULL,
  AttendYear INT NOT NULL,
  GraduateYear INT NOT NULL,
  MSSV NVARCHAR(50) NOT NULL,
  IDFalcuty INT NOT NULL,
  IDContract INT NOT NULL,
  IDRoom INT NOT NULL,
  [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDSinhVien),
  FOREIGN KEY (IDFalcuty) REFERENCES Faculty(IDFalcuty),
  FOREIGN KEY (IDContract) REFERENCES Contract(IDContract),
  FOREIGN KEY (IDRoom) REFERENCES Room(IDRoom)
);


CREATE TABLE Account
(
  IDAccount INT NOT NULL IDENTITY(1,1),
  Email NVARCHAR(50) NOT NULL,
  Password TEXT NOT NULL,
  Available BINARY NOT NULL,
  Name TEXT NOT NULL,
  IDSinhVien INT NOT NULL,
   [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDAccount),
  FOREIGN KEY (IDSinhVien) REFERENCES SinhVien(IDSinhVien)
);

CREATE TABLE AccountType
(
  IDAccountType INT NOT NULL IDENTITY(1,1),
  TypeDescription TEXT NOT NULL,
  IDAccount INT NOT NULL,
   [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDAccountType),
  FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount)
);

CREATE TABLE Mistake
(
  IDMistake INT NOT NULL IDENTITY(1,1),
  MistakeDes NTEXT NOT NULL,
  TimeCaught DATETIME NOT NULL,
  BedID NVARCHAR(10) NOT NULL,
  IDSinhVien INT NOT NULL,
  IDAccount INT NOT NULL,
  IDRoom INT NOT NULL,
  [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDMistake),
  FOREIGN KEY (IDSinhVien) REFERENCES SinhVien(IDSinhVien),
  FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount),
  FOREIGN KEY (IDRoom) REFERENCES Room(IDRoom)
);


CREATE TABLE Fee
(
  IDFee INT NOT NULL IDENTITY(1,1),
  Name NTEXT NOT NULL,
  Description NTEXT NOT NULL,
  DateStart DATETIME NOT NULL,
  DateEnd DATETIME NOT NULL,
  Status int NOT NULL,
  Quantity INT NOT NULL,
  IDRoom INT NOT NULL,
  [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDFee),
  FOREIGN KEY (IDRoom) REFERENCES Room(IDRoom),
);

Create Table Post(
	IDPost int identity Primary key,
	PostTitle text NOT NULL,
	PostDetail text NOT NULL,
	IDAccount int,
	FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount),
	[Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
);
Create Table Category(
	IDCategory int identity Primary key,
	CategoryTitle text NOT NULL,
	CategoryDetail text NOT NULL,
	ColorChip text,
	[Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
);
Create Table CategoryBridge(
	IDCategory int NOT NULL,
	IDPost int NOT NULL,
	[Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
	PRIMARY KEY (IDCategory, IDPost),
	FOREIGN KEY (IDCategory) REFERENCES Category(IDCategory),
	FOREIGN KEY (IDPost) REFERENCES Post(IDPost),
);
Create Table Attendance(
	IDAttendance int primary key identity,
	IsAttend int NOT NULL,
	Reason ntext,
	[Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
);
Create Table AttendanceBridge(
	IDAttendance int NOT NULL,
	IDSinhVien int NOT NULL,
	IDAccount int NOT NULL,
	PRIMARY KEY (IDAttendance, IDSinhVien,IDAccount),
	FOREIGN KEY (IDAttendance) REFERENCES Attendance(IDAttendance),
	FOREIGN KEY (IDSinhVien) REFERENCES SinhVien(IDSinhVien),
	FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount),
)
CREATE TABLE Log
(
  IDLog INT NOT NULL IDENTITY(1,1),
  IDFee INT NOT NULL,
  DateDone DATETIME NOT NULL,
  Quantity INT NOT NULL,
  IDSinhVien INT NOT NULL,
  [Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
  PRIMARY KEY (IDLog),
  FOREIGN KEY (IDSinhVien) REFERENCES SinhVien(IDSinhVien),
  FOREIGN KEY (IDFee) REFERENCES Fee(IDFee)
);
create table TrucCong(
	IDTruc INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	IDAccount INT,
	DateApply datetime,
	[Order] int,
	[Meta] text,
	DateBegin datetime NOT NULL,
	Hide int NOT NULL,
	FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount)
)
-----Data-----
USE [KTXTDTU]
GO

 
INSERT INTO [dbo].[Place]
           ([IDPlace]
           ,[Description]
           ,[Order]
           ,[Meta]
           ,[DateBegin]
           ,[Hide])
     VALUES
           ('KV3'
           ,N'Khu vực 3'
           ,null
           ,null
           ,GETDATE()
           ,0)
GO
INSERT INTO [dbo].[Priority]
           ([IDPriority]
           ,[PriorityDescription]
           ,[Order]
           ,[Meta]
           ,[DateBegin]
           ,[Hide])
     VALUES
           (11
           ,N'Sinh viên khu vực tuyển sinh theo thứ tự: KV1 không thuộc thành phố, thị xã, KV2-NT không thuộc thành phố, thị xã; KV1 thuộc thành phố, thị xã; KV2-NT thuộc thành phố, thị xã; KV2; KV3'
           ,null
           ,null
           ,GETDATE()
           ,0)
GO