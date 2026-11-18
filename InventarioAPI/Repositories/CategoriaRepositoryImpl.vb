
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore
Imports InventarioAPI.Data
Imports InventarioAPI.Models

Namespace Repositories

    Public Class CategoriaRepositoryImpl
        Implements ICategoriaRepository

        Private ReadOnly _context As InventarioDbContext

        Public Sub New(context As InventarioDbContext)
            _context = context
        End Sub

        Public Async Function ObtenerTodos() As Task(Of List(Of Categoria)) _
            Implements ICategoriaRepository.ObtenerTodos

            Return Await _context.Categorias.ToListAsync()

        End Function

        Public Async Function ObtenerPorId(id As Integer) As Task(Of Categoria) _
            Implements ICategoriaRepository.ObtenerPorId

            Return Await _context.Categorias.
                Include(Function(c) c.Productos).
                FirstOrDefaultAsync(Function(c) c.Id = id)

        End Function

        Public Async Function Agregar(categoria As Categoria) As Task _
            Implements ICategoriaRepository.Agregar

            _context.Categorias.Add(categoria)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function Actualizar(categoria As Categoria) As Task _
            Implements ICategoriaRepository.Actualizar

            _context.Categorias.Update(categoria)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function Eliminar(categoria As Categoria) As Task _
            Implements ICategoriaRepository.Eliminar

            _context.Categorias.Remove(categoria)
            Await _context.SaveChangesAsync()

        End Function

        Public Async Function ExisteNombre(nombre As String,
                    Optional excluirId As Integer = 0) As Task(Of Boolean) _
                    Implements ICategoriaRepository.ExisteNombre

            Return Await _context.Categorias.AnyAsync(
                Function(c) c.Nombre.ToLower() = nombre.ToLower() AndAlso
                c.Id <> excluirId)

        End Function

    End Class

End Namespace
