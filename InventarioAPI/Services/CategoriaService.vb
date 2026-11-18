
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports InventarioAPI.Models
Imports InventarioAPI.DTOs
Imports InventarioAPI.Repositories

Namespace Services

    Public Interface ICategoriaService
        Function ObtenerTodos() As Task(Of List(Of CategoriaResponseDto))
        Function ObtenerPorId(id As Integer) As Task(Of CategoriaResponseDto)
        Function Agregar(dto As CategoriaCreateDto) As Task(Of CategoriaResponseDto)
        Function Actualizar(id As Integer, dto As CategoriaUpdateDto) As Task(Of CategoriaResponseDto)
        Function Eliminar(id As Integer) As Task
    End Interface

    Public Class CategoriaService
        Implements ICategoriaService

        Private ReadOnly _repository As ICategoriaRepository

        Public Sub New(repository As ICategoriaRepository)
            _repository = repository
        End Sub

        Public Async Function ObtenerTodos() As Task(Of List(Of CategoriaResponseDto)) _
            Implements ICategoriaService.ObtenerTodos

            Dim categorias = Await _repository.ObtenerTodos()

            Return categorias.Select(Function(c) New CategoriaResponseDto With {
                .Id = c.Id,
                .Nombre = c.Nombre,
                .Descripcion = c.Descripcion
            }).ToList()

        End Function

        Public Async Function ObtenerPorId(id As Integer) As Task(Of CategoriaResponseDto) _
            Implements ICategoriaService.ObtenerPorId

            Dim c = Await _repository.ObtenerPorId(id)

            If c Is Nothing Then Return Nothing

            Return New CategoriaResponseDto With {
                .Id = c.Id,
                .Nombre = c.Nombre,
                .Descripcion = c.Descripcion,
                .Productos = c.Productos.Select(Function(p) New ProductoResumenDto With {
                    .Id = p.Id,
                    .Nombre = p.Nombre,
                    .Precio = p.Precio,
                    .Stock = p.Stock
                }).ToList()
            }

        End Function

        Public Async Function Agregar(dto As CategoriaCreateDto) As Task(Of CategoriaResponseDto) _
            Implements ICategoriaService.Agregar

            If Await _repository.ExisteNombre(dto.Nombre.Trim()) Then
                Throw New InvalidOperationException("Ya existe una categoría con ese nombre.")
            End If

            Dim categoria As New Categoria With {
                .Nombre = dto.Nombre.Trim(),
                .Descripcion = dto.Descripcion
            }

            Await _repository.Agregar(categoria)

            Return Await ObtenerPorId(categoria.Id)

        End Function

        Public Async Function Actualizar(id As Integer, dto As CategoriaUpdateDto) As Task(Of CategoriaResponseDto) _
            Implements ICategoriaService.Actualizar

            Dim categoria = Await _repository.ObtenerPorId(id)

            If categoria Is Nothing Then Return Nothing

            If Await _repository.ExisteNombre(dto.Nombre.Trim(), id) Then
                Throw New InvalidOperationException("Ya existe otra categoría con ese nombre.")
            End If

            categoria.Nombre = dto.Nombre.Trim()
            categoria.Descripcion = dto.Descripcion

            Await _repository.Actualizar(categoria)

            Return Await ObtenerPorId(id)

        End Function

        Public Async Function Eliminar(id As Integer) As Task _
            Implements ICategoriaService.Eliminar

            Dim categoria = Await _repository.ObtenerPorId(id)

            If categoria Is Nothing Then
                Throw New KeyNotFoundException("Categoría no encontrada.")
            End If

            If categoria.Productos IsNot Nothing AndAlso categoria.Productos.Any() Then
                Throw New InvalidOperationException("No se puede eliminar una categoría que tiene productos asociados.")
            End If

            Await _repository.Eliminar(categoria)

        End Function

    End Class

End Namespace
