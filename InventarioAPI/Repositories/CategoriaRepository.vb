
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports InventarioAPI.Models

Namespace Repositories

    Public Interface ICategoriaRepository

        Function ObtenerTodos() As Task(Of List(Of Categoria))

        Function ObtenerPorId(id As Integer) As Task(Of Categoria)

        Function Agregar(categoria As Categoria) As Task

        Function Actualizar(categoria As Categoria) As Task

        Function Eliminar(categoria As Categoria) As Task

        Function ExisteNombre(nombre As String,
                              Optional excluirId As Integer = 0) As Task(Of Boolean)

    End Interface

End Namespace
