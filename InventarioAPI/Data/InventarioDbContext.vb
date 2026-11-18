
Imports Microsoft.EntityFrameworkCore
Imports InventarioAPI.Models

Namespace Data

    Public Class InventarioDbContext
        Inherits DbContext

        Public Sub New(options As DbContextOptions(Of InventarioDbContext))
            MyBase.New(options)
        End Sub

        Public Property Categorias As DbSet(Of Categoria)

        Public Property Productos As DbSet(Of Producto)

        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)

            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of Categoria)().
                HasMany(Function(c) c.Productos).
                WithOne(Function(p) p.Categoria).
                HasForeignKey(Function(p) p.CategoriaId).
                IsRequired().
                OnDelete(DeleteBehavior.Restrict)

            modelBuilder.Entity(Of Producto)().
                Property(Function(p) p.Precio).
                HasColumnType("decimal(18,2)")

        End Sub

    End Class

End Namespace
