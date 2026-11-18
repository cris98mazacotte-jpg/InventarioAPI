
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports InventarioAPI.Models

Namespace Repositories

    Public Interface IProductoRepository

        Function ObtenerTodos() As Task(Of List(Of Producto))

        Function ObtenerPorId(id As Integer) As Task(Of Producto)

        Function ObtenerPorCategoria(categoriaId As Integer) As Task(Of List(Of Producto))

        Function Agregar(producto As Producto) As Task

        Function Actualizar(producto As Producto) As Task

        Function Eliminar(producto As Producto) As Task

        Function ExisteNombre(nombre As String,
                              Optional excluirId As Integer = 0) As Task(Of Boolean)

    End Interface

End Namespace
