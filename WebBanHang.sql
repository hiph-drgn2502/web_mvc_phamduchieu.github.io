create database WebBanHang
use WebBanHang
go
create table Brand (
	Id int identity(1,1) primary key,
	BrandName nvarchar(20) ,
	Avartar nvarchar(20),
	Slug nvarchar(30),
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime,
	Deleted bit
);

create table Category (
	Id int identity(1,1) primary key,
	CategoryName nvarchar(20) ,
	Avartar nvarchar(20),
	Slug nvarchar(20),
	ShowOnHomePage bit,
	DisplayOrder int,
	Deleted bit,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);

create table Orders (
	Id int identity(1,1) primary key,
	OrderName nvarchar(100) ,
	ProductId int,
	Price float,
	OrderStatus int,
	CreatedOnUtc datetime,
);

create table OrderDetail (
	Id int identity(1,1) primary key,
	OrderId int ,
	ProductId int,
	Quantity int,
);

alter table OrderDetail
alter column OrderId int



create table Product (
	Id int identity(1,1) primary key,
	ProductName nvarchar(20),
	Avartar nvarchar(20),
	CategoryId int,
	ShortDes nvarchar(100),
	FullDescription nvarchar(100),
	Price float,
	PriceDiscount float,
	TypeId int,
	Slug nvarchar(20),
	BrandId int,
	Deleted bit,
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);
alter table Product
alter column Slug nvarchar(20)


create table SaleProduct (
	Id int identity(1,1) primary key,
	ProductName nvarchar(100),
	Avartar nvarchar(100),
	CategoryId int,
	ShortDes nvarchar(100),
	FullDescription nvarchar(500),
	Price float,
	PriceDiscount float,
	TypeId int,
	Slug nvarchar(100),
	BrandId int,
	Deleted bit,
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);

create table ProductBrand (
	Id int identity(1,1) primary key,
	ProductName nvarchar(100),
	Avartar nvarchar(100),
	CategoryId int,
	ShortDes nvarchar(100),
	FullDescription nvarchar(500),
	Price float,
	PriceDiscount float,
	TypeId int,
	Slug nvarchar(100),
	BrandId int,
	Deleted bit,
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);

create table ProductAccessory (
	Id int identity(1,1) primary key,
	ProductName nvarchar(100),
	Avartar nvarchar(100),
	CategoryId int,
	ShortDes nvarchar(100),
	FullDescription nvarchar(500),
	Price float,
	PriceDiscount float,
	TypeId int,
	Slug nvarchar(100),
	BrandId int,
	Deleted bit,
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);

create table ProductRecommended (
	Id int identity(1,1) primary key,
	ProductName nvarchar(100),
	Avartar nvarchar(100),
	CategoryId int,
	ShortDes nvarchar(100),
	FullDescription nvarchar(500),
	Price float,
	PriceDiscount float,
	TypeId int,
	Slug nvarchar(100),
	BrandId int,
	Deleted bit,
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime
);
create table Users (
	Id int identity(1,1) primary key,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Email nvarchar(50),
	PasswordUser nvarchar(50),
	IsAdmin bit,
);

--INSERT ITEM

Insert into Brand (BrandName ,Avartar ,Slug ,ShowOnHomePage,DisplayOrder,CreatedOnUtc,UpdatedOnUtc,Deleted)
Values('Quan','quan.jpg','quan', 'True', '1', 'null', 'null', 'False');



--
select *
from Category