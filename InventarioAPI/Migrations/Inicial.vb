
Imports Microsoft.EntityFrameworkCore.Infrastructure
Imports Microsoft.EntityFrameworkCore.Migrations
Imports InventarioAPI.Data

Namespace Migrations

    <DbContext(GetType(InventarioDbContext))>
    <Migration("20261118010000_Inicial")>
    Public Class Inicial
        Inherits Migration

        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)

            migrationBuilder.Sql("CREATE TABLE [Categorias] ([Id] int NOT NULL IDENTITY(1,1), [Nombre] nvarchar(50) NOT NULL, [Descripcion] nvarchar(200) NULL, CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id]));")

            migrationBuilder.Sql("CREATE TABLE [Productos] ([Id] int NOT NULL IDENTITY(1,1), [Nombre] nvarchar(50) NOT NULL, [Descripcion] nvarchar(200) NULL, [Precio] decimal(18,2) NOT NULL, [Stock] int NOT NULL, [FechaRegistro] datetime2 NOT NULL, [CategoriaId] int NOT NULL, CONSTRAINT [PK_Productos] PRIMARY KEY ([Id]), CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION);")

            migrationBuilder.Sql("CREATE INDEX [IX_Productos_CategoriaId] ON [Productos] ([CategoriaId]);")

        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)

            migrationBuilder.Sql("DROP TABLE [Productos];")
            migrationBuilder.Sql("DROP TABLE [Categorias];")

        End Sub

    End Class

End Namespace
