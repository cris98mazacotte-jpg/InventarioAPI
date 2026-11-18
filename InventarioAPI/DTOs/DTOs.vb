
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Namespace DTOs

    ' DTO para registrar categorias
    Public Class CategoriaCreateDto

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

    End Class

    ' DTO para modificar categorias
    Public Class CategoriaUpdateDto

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

    End Class

    ' DTO para responder categorias
    Public Class CategoriaResponseDto

        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property Productos As List(Of ProductoResumenDto) =
            New List(Of ProductoResumenDto)

    End Class

    ' DTO para registrar productos
    Public Class ProductoCreateDto

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

        <Range(1, 999999999)>
        Public Property Precio As Decimal

        <Range(0, Integer.MaxValue)>
        Public Property Stock As Integer

        <Range(1, Integer.MaxValue)>
        Public Property CategoriaId As Integer

    End Class

    ' DTO para modificar productos
    Public Class ProductoUpdateDto

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

        <Range(1, 999999999)>
        Public Property Precio As Decimal

        <Range(0, Integer.MaxValue)>
        Public Property Stock As Integer

        <Range(1, Integer.MaxValue)>
        Public Property CategoriaId As Integer

    End Class

    ' DTO para responder productos
    Public Class ProductoResponseDto

        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property Precio As Decimal
        Public Property Stock As Integer
        Public Property FechaRegistro As DateTime
        Public Property CategoriaId As Integer
        Public Property CategoriaNombre As String

    End Class

    ' DTO resumido para productos dentro de una categoria
    Public Class ProductoResumenDto

        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Precio As Decimal
        Public Property Stock As Integer

    End Class

End Namespace
