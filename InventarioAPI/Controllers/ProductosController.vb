
Imports Microsoft.AspNetCore.Mvc
Imports System.Threading.Tasks
Imports InventarioAPI.DTOs
Imports InventarioAPI.Services

Namespace Controllers

    <ApiController>
    <Route("api/[controller]")>
    Public Class ProductosController
        Inherits ControllerBase

        Private ReadOnly _service As IProductoService

        Public Sub New(service As IProductoService)
            _service = service
        End Sub

        ' GET: api/Productos
        <HttpGet>
        Public Async Function ObtenerTodos() As Task(Of IActionResult)

            Dim productos = Await _service.ObtenerTodos()

            Return Ok(productos)

        End Function

        ' GET: api/Productos/5
        <HttpGet("{id}")>
        Public Async Function ObtenerPorId(id As Integer) As Task(Of IActionResult)

            Dim producto = Await _service.ObtenerPorId(id)

            If producto Is Nothing Then
                Return NotFound("Producto no encontrado.")
            End If

            Return Ok(producto)

        End Function

        ' GET: api/Productos/categoria/5
        <HttpGet("categoria/{categoriaId}")>
        Public Async Function ObtenerPorCategoria(
            categoriaId As Integer) As Task(Of IActionResult)

            Dim productos = Await _service.ObtenerPorCategoria(categoriaId)

            If productos Is Nothing Then
                Return NotFound("Categoría no encontrada.")
            End If

            Return Ok(productos)

        End Function

        ' POST: api/Productos
        <HttpPost>
        Public Async Function Agregar(
            dto As ProductoCreateDto) As Task(Of IActionResult)

            Dim producto = Await _service.Agregar(dto)

            Return CreatedAtAction(
                NameOf(ObtenerPorId),
                New With {.id = producto.Id},
                producto)

        End Function

        ' PUT: api/Productos/5
        <HttpPut("{id}")>
        Public Async Function Actualizar(
            id As Integer,
            dto As ProductoUpdateDto) As Task(Of IActionResult)

            Dim producto = Await _service.Actualizar(id, dto)

            If producto Is Nothing Then
                Return NotFound("Producto no encontrado.")
            End If

            Return Ok(producto)

        End Function

        ' DELETE: api/Productos/5
        <HttpDelete("{id}")>
        Public Async Function Eliminar(id As Integer) As Task(Of IActionResult)

            Await _service.Eliminar(id)

            Return NoContent()

        End Function

    End Class

End Namespace
