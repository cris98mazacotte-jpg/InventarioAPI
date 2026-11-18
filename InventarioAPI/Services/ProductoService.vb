
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports InventarioAPI.Models
Imports InventarioAPI.DTOs
Imports InventarioAPI.Repositories

Namespace Services

    Public Interface IProductoService
        Function ObtenerTodos() As Task(Of List(Of ProductoResponseDto))
        Function ObtenerPorId(id As Integer) As Task(Of ProductoResponseDto)
        Function ObtenerPorCategoria(categoriaId As Integer) As Task(Of List(Of ProductoResponseDto))
        Function Agregar(dto As ProductoCreateDto) As Task(Of ProductoResponseDto)
        Function Actualizar(id As Integer, dto As ProductoUpdateDto) As Task(Of ProductoResponseDto)
        Function Eliminar(id As Integer) As Task
    End Interface

    Public Class ProductoService
        Implements IProductoService

        Private ReadOnly _repository As IProductoRepository
        Private ReadOnly _categoriaRepository As ICategoriaRepository

        Public Sub New(repository As IProductoRepository,
                       categoriaRepository As ICategoriaRepository)
            _repository = repository
            _categoriaRepository = categoriaRepository
        End Sub

        Private Function ConvertirDto(p As Producto) As ProductoResponseDto

            Return New ProductoResponseDto With {
                .Id = p.Id,
                .Nombre = p.Nombre,
                .Descripcion = p.Descripcion,
                .Precio = p.Precio,
                .Stock = p.Stock,
                .FechaRegistro = p.FechaRegistro,
                .CategoriaId = p.CategoriaId,
                .CategoriaNombre = If(p.Categoria Is Nothing, Nothing, p.Categoria.Nombre)
            }

        End Function

        Public Async Function ObtenerTodos() As Task(Of List(Of ProductoResponseDto)) _
            Implements IProductoService.ObtenerTodos

            Dim productos = Await _repository.ObtenerTodos()

            Return productos.Select(Function(p) ConvertirDto(p)).ToList()

        End Function

        Public Async Function ObtenerPorId(id As Integer) As Task(Of ProductoResponseDto) _
            Implements IProductoService.ObtenerPorId

            Dim p = Await _repository.ObtenerPorId(id)

            If p Is Nothing Then Return Nothing

            Return ConvertirDto(p)

        End Function

        Public Async Function ObtenerPorCategoria(categoriaId As Integer) As Task(Of List(Of ProductoResponseDto)) _
            Implements IProductoService.ObtenerPorCategoria

            Dim categoria = Await _categoriaRepository.ObtenerPorId(categoriaId)

            If categoria Is Nothing Then Return Nothing

            Dim productos = Await _repository.ObtenerPorCategoria(categoriaId)

            Return productos.Select(Function(p) ConvertirDto(p)).ToList()

        End Function

        Public Async Function Agregar(dto As ProductoCreateDto) As Task(Of ProductoResponseDto) _
            Implements IProductoService.Agregar

            If dto.Stock < 0 Then
                Throw New ArgumentException("El stock no puede ser negativo.")
            End If

            If dto.Precio <= 0 Then
                Throw New ArgumentException("El precio debe ser mayor que cero.")
            End If

            If Await _repository.ExisteNombre(dto.Nombre.Trim()) Then
                Throw New InvalidOperationException("Ya existe un producto con ese nombre.")
            End If

            Dim categoria = Await _categoriaRepository.ObtenerPorId(dto.CategoriaId)

            If categoria Is Nothing Then
                Throw New KeyNotFoundException("La categoría indicada no existe.")
            End If

            Dim producto As New Producto With {
                .Nombre = dto.Nombre.Trim(),
                .Descripcion = dto.Descripcion,
                .Precio = dto.Precio,
                .Stock = dto.Stock,
                .FechaRegistro = DateTime.Now,
                .CategoriaId = dto.CategoriaId
            }

            Await _repository.Agregar(producto)

            Return Await ObtenerPorId(producto.Id)

        End Function

        Public Async Function Actualizar(id As Integer, dto As ProductoUpdateDto) As Task(Of ProductoResponseDto) _
            Implements IProductoService.Actualizar

            Dim producto = Await _repository.ObtenerPorId(id)

            If producto Is Nothing Then Return Nothing

            If dto.Stock < 0 Then
                Throw New ArgumentException("El stock no puede ser negativo.")
            End If

            If dto.Precio <= 0 Then
                Throw New ArgumentException("El precio debe ser mayor que cero.")
            End If

            If Await _repository.ExisteNombre(dto.Nombre.Trim(), id) Then
                Throw New InvalidOperationException("Ya existe otro producto con ese nombre.")
            End If

            Dim categoria = Await _categoriaRepository.ObtenerPorId(dto.CategoriaId)

            If categoria Is Nothing Then
                Throw New KeyNotFoundException("La categoría indicada no existe.")
            End If

            producto.Nombre = dto.Nombre.Trim()
            producto.Descripcion = dto.Descripcion
            producto.Precio = dto.Precio
            producto.Stock = dto.Stock
            producto.CategoriaId = dto.CategoriaId

            Await _repository.Actualizar(producto)

            Return Await ObtenerPorId(id)

        End Function

        Public Async Function Eliminar(id As Integer) As Task _
            Implements IProductoService.Eliminar

            Dim producto = Await _repository.ObtenerPorId(id)

            If producto Is Nothing Then
                Throw New KeyNotFoundException("Producto no encontrado.")
            End If

            Await _repository.Eliminar(producto)

        End Function

    End Class

End Namespace
