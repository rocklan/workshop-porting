create table Users
(	UserID int primary key identity(1,1),
	Username nvarchar(100),
	Password nvarchar(255),
	QrCode nvarchar(255)
)
alter table Users
add Attempts int not null default(0)

alter table Users
add LockedOutUntil datetime null


insert into Users (USername, password, qrcode) values ('lachlan', null, null)