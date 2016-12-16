Create Database DealQuestionAnswer

use DealQuestionAnswer

create table Users
(
	Id int identity primary key,
	Name nvarchar(50) not null,
	Email varchar(50) not null unique,
	[Password] varchar(20) not null,
	UserType int
)
go
create table Deals
(
	Id int identity primary key,
	Name nvarchar(100) not null,
	Price decimal not null,
	Details nvarchar(500),
	MerchantId int foreign key references Users(Id)
)
go
create table Questions
(
	Id int identity primary key,
	Question nvarchar(1000) not null,
	UserId int foreign key references Users(Id),
	DealId int foreign key references Deals(Id),
	[DateTime] smalldatetime default getdate(),
	IsActive bit default 1
)

update Questions set Question='Hello' where Id=1
select * from Questions where [DateTime] between '2016-10-16' and '2016-10-18'
go
create table Answers
(
	Id int identity primary key,
	Answer nvarchar(1000) not null,
	QuestionId int foreign key references Questions(Id),
	UserId int foreign key references Users(Id),
	[DateTime] smalldatetime default getdate(),
	IsActive bit default 1
)
go
create table Replys
(
	Id int identity primary key,
	Reply nvarchar(1000) not null,
	AnswerId int foreign key references Answers(Id),
	UserId int foreign key references Users(Id),
	[DateTime] smalldatetime default getdate(),
	IsActive bit default 1
)
go
create table QuestionVotes
(
	Id int identity primary key,
	UpVote tinyint,
	DownVote tinyint,
	QuestionId int foreign key references Questions(Id),
	UserId int foreign key references Users(Id),
	IsUpVote bit,
	IsDownVote bit
)
go
create table AnswerVotes
(
	Id int identity primary key,
	UpVote tinyint,
	DownVote tinyint,
	AnswerId int foreign key references Answers(Id),
	UserId int foreign key references Users(Id),
	IsUpVote bit,
	IsDownVote bit
)

create procedure spGetQuestions
@id int
as
select q.Id, q.Question,q.UserId,q.[DateTime],u.Name,
(select COUNT(UpVote) from QuestionVotes qv where qv.QuestionId=q.Id and qv.IsUpVote=1) as PositiveCount,
(select COUNT(DownVote) from QuestionVotes qv where qv.QuestionId=q.Id and qv.IsDownVote=1) as NegativeCount
from Questions q join Users u on u.Id=q.UserId where q.DealId=@id

Create proc spGetAnswers
@id int
as
select a.Id, a.Answer,a.[DateTime], u.Name,
(select COUNT(UpVote) from AnswerVotes av where av.AnswerId=a.Id and IsUpVote=1) as PostiveCount,
(select COUNT(DownVote) from AnswerVotes av where av.AnswerId=a.Id and IsDownVote=1) as NegativeCount
from Answers a join Users u on u.Id = a.UserId where a.QuestionId = @id and a.IsActive=1

create proc spGetReplys
@id int
as
select a.Id, a.Reply,a.[DateTime],u.Name 
from Replys a join Users u on u.Id = a.UserId where a.AnswerId=@id and a.IsActive=1

select * from Answers

select * from Replys

alter proc spLoginUser
@email varchar(50),
@pass varchar(20)
as
select Id,Email,UserType from Users where Email= @email and [Password] = @pass


spLoginUser 'imran@gmail.com','123456'