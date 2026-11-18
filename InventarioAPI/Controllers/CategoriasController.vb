Imports Microsoft.AspNetCore.Mvc
Imports System.Threading.Tasks
Imports InventarioAPI.DTOs
Imports InventarioAPI.Services

Namespace Controllers

    <ApiController>
    <Route("api/[controller]")>
    Public Class CategoriasController
        Inherits ControllerBase

        Private ReadOnly _service As ICategoriaService

        Public Sub New(service As ICategoriaService)
            _service = service
        End Sub

        ' GET: api/Categorias
        <HttpGet>
        Public Async Function ObtenerTodos() As Task(Of IActionResult)

            Dim categorias = Await _service.ObtenerTodos()

            Return Ok(categorias)

        End Function

        ' GET: api/Categorias/5
        <HttpGet("{id}")>
        Public Async Function ObtenerPorId(id As Integer) As Task(Of IActionResult)

            Dim categoria = Await _service.ObtenerPorId(id)

            If categoria Is Nothing Then
                Return NotFound("Categoría no encontrada.")
            End If

            Return Ok(categoria)

        End Function

        ' POST: api/Categorias
        <HttpPost>
        Public Async Function Agregar(
            dto As CategoriaCreateDto) As Task(Of IActionResult)

            Dim categoria = Await _service.Agregar(dto)

            Return CreatedAtAction(
                NameOf(ObtenerPorId),
                New With {.id = categoria.Id},
                categoria)

        End Function

        ' PUT: api/Categorias/5
        <HttpPut("{id}")>
        Public Async Function Actualizar(
            id As Integer,
            dto As CategoriaUpdateDto) As Task(Of IActionResult)

            Dim categoria = Await _service.Actualizar(id, dto)

            If categoria Is Nothing Then
                Return NotFound("Categoría no encontrada.")
            End If

            Return Ok(categoria)

        End Function

        ' DELETE: api/Categorias/5
        <HttpDelete("{id}")>
        Public Async Function Eliminar(id As Integer) As Task(Of IActionResult)

            Await _service.Eliminar(id)

            Return NoContent()

        End Function

    End Class

End Namespace