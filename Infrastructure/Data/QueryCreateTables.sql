CREATE TABLE Brands
(
	Id int not null identity primary key,
	BrandName nvarchar(50) not null
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	ParentCategoryId int null references Categories(Id),
	CategoryName nvarchar(50) not null, 
)

CREATE TABLE Products
(
	Id nvarchar(100) not null primary key,
	ProductName nvarchar(50) not null
)

CREATE TABLE ProductCategories
(
	ProductId nvarchar(100) not null references Products(Id),
	CategoryId int not null references Categories(Id)

	primary key(ProductId, CategoryId)
)

CREATE TABLE ProductDetails
(
	ProductId nvarchar(100) not null references Products(Id) primary key,
	BrandId int not null references Brands(Id),
	UnitPrice money not null,
	Color nvarchar(20) null,
	Size nvarchar(10) null
)