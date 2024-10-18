/*
Created		06/10/2024
Modified		06/10/2024
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/

Create Database QLSANPHAM
USE QLSANPHAM

Create table [LOAISP]
(
	[MALOAI] Char(2) NOT NULL,
	[TENLOAI] NVarchar(30) NOT NULL,
Primary Key ([MALOAI])
) 
go

Create table [SANPHAM]
(
	[MASP] Char(6) NOT NULL,
	[TENSP] Nvarchar(30) NOT NULL,
	[NGAYNHAP] Datetime NOT NULL,
	[MALOAI] Char(2) NOT NULL,
Primary Key ([MASP])
) 
go


Alter table [SANPHAM] add  foreign key([MALOAI]) references [LOAISP] ([MALOAI])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go
