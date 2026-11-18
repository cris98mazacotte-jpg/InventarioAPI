
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore
Imports InventarioAPI.Data
Imports InventarioAPI.Models

Namespace Repositories

    Public Class ProductoRepositoryImpl
        Implements IProductoRepository

        Private ReadOnly _context As InventarioDbContext

        Public Sub New(context As InventarioDbContext)
            _context = context
        End Sub

        Public Async Function ObtenerTodos() As Task(Of List(Of Producto)) _
            Implements IProductoRepository.ObtenerTodos

            Return Await _context.Productos.
                Include(Function(p) p.Categoria).
                ToListAsync()

        End Function

        Public Async Function ObtenerPorId(id As Integer) As Task(Of Producto) _
            Implements IProductoRepository.ObtenerPorId

            Return Await _context.Productos.
                Include(Function(p) p.Categoria).
                FirstOrDefaultAsync(Function(p) p.Id = id)

        End Function

        Public Async Function ObtenerPorCategoria(categoriaId As Integer) As Task(Of List(Of Producto)) _
            Implements IProductoRepository.ObtenerPorCategoria

            Return Await _context.Productos.
                Include(Function(p) p.Categoria).
                Where(Function(p) p.CategoriaId = categoriaId).
                ToListAsync()

        End Function

        Public Async Function Agregar(producto As Producto) As Task _
            Implements IProductoRepository.Agregar

            _context.Productos.Add(producto)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function Actualizar(producto As Producto) As Task _
            Implements IProductoRepository.Actualizar

            _context.Productos.Update(producto)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function Eliminar(producto As Producto) As Task _
            Implements IProductoRepository.Eliminar

            _context.Productos.Remove(producto)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function ExisteNombre(nombre As String,
                    Optional excluirId As Integer = 0) As Task(Of Boolean) _
                    Implements IProductoRepository.ExisteNombre

            Return Await _context.Productos.AnyAsync(
                Function(p) p.Nombre.ToLower() = nombre.ToLower() AndAlso
                p.Id <> excluirId)

        End Function

    End Class

End Namespace
