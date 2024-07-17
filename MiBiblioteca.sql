-- 11/07/2024 --

Create Database MiBiblioteca
Go

Use MiBiblioteca
Go

--------------------------------
Create Table Paises (
    PaisID Int Identity(1,1) Not Null Primary Key,
    NombrePais NVarchar(Max) Not Null
)
Go


Select * From Paises
Go

--------------------------------
Create Table Autores (
    AutorID Int Identity(1,1) Not Null Primary Key,
    NombresAutor NVarchar(Max) Not Null,
    ApellidosAutor NVarchar(Max) Not Null,
    PaisID Int Null References Paises(PaisID) On Delete Set Null
)
Go


Select * From Autores
Go


-- 12/07/2024 --

Create Table Generos (
    GeneroID Int Identity(1,1) Not Null Primary Key,
    NombreGenero NVarchar(Max) Not Null
)
Go


Select * From Generos
Go

--------------------------------

Create Table Libros (
    LibroID Int Identity(1,1) Not Null Primary Key,
    Titulo NVarchar(Max) Not Null,
    AñoPublicacion Int Not Null,
	Portada NVarchar(Max),
	Descripcion NVarchar(Max)
)
Go


Select * From Libros
Go

--------------------------------


Create Table GenerosLibro (
	Primary Key (LibroID, GeneroID),
    LibroID Int Not Null References Libros(LibroID),
    GeneroID Int Not Null References Generos(GeneroID)
)
Go


Select * From GenerosLibro
Go

--------------------------------

Create Table AutoresLibro (
	Primary Key (LibroID, AutorID), 
    LibroID Int Not Null References Libros(LibroID),
    AutorID Int Not Null References Autores(AutorID)
)
Go


Select * From AutoresLibro
Go

