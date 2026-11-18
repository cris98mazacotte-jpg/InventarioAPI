
Imports System
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    Public Class Producto

        Public Property Id As Integer

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

        <Range(GetType(Decimal), "0.01", "999999999")>
        <Column(TypeName:="decimal(18,2)")>
        Public Property Precio As Decimal

        <Range(0, Integer.MaxValue)>
        Public Property Stock As Integer

        Public Property FechaRegistro As DateTime

        <Required>
        Public Property CategoriaId As Integer

        Public Overridable Property Categoria As Categoria

    End Class

End Namespace
