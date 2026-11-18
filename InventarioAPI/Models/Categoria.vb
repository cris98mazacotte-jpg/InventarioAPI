
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Namespace Models

    Public Class Categoria

        Public Property Id As Integer

        <Required>
        <StringLength(50)>
        Public Property Nombre As String

        <StringLength(200)>
        Public Property Descripcion As String

        Public Overridable Property Productos As ICollection(Of Producto)

    End Class

End Namespace
