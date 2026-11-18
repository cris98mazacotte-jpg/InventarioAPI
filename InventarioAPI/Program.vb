
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Configuration
Imports Microsoft.EntityFrameworkCore
Imports InventarioAPI.Data
Imports InventarioAPI.Repositories
Imports InventarioAPI.Services
Imports InventarioAPI.Middleware

Module Program

    Sub Main(args As String())

        Dim builder = WebApplication.CreateBuilder(args)

        Dim conexion = builder.Configuration.GetConnectionString("ConexionSQL")

        builder.Services.AddDbContext(Of InventarioDbContext)(
            Sub(options)
                options.UseSqlServer(conexion)
            End Sub)

        ' Repositories
        builder.Services.AddScoped(Of ICategoriaRepository, CategoriaRepositoryImpl)()
        builder.Services.AddScoped(Of IProductoRepository, ProductoRepositoryImpl)()

        ' Services
        builder.Services.AddScoped(Of ICategoriaService, CategoriaService)()
        builder.Services.AddScoped(Of IProductoService, ProductoService)()

        ' Controllers y Swagger
        builder.Services.AddControllers()
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Dim app = builder.Build()

        ' Manejo global de excepciones
        app.UseMiddleware(Of ExceptionMiddleware)()

        app.UseSwagger()
        app.UseSwaggerUI()

        app.MapControllers()

        app.MapGet("/", Function() "API Inventario funcionando correctamente")

        app.Run()

    End Sub

End Module
