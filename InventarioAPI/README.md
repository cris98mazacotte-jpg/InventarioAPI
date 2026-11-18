# API REST - Sistema de Gestión de Inventario

## Descripción

API REST desarrollada en Visual Basic .NET con ASP.NET Core Web API para administrar categorías y productos de un sistema de inventario.

La aplicación permite realizar operaciones CRUD, validar datos y aplicar reglas de negocio para garantizar la integridad de la información.

## Tecnologías utilizadas

- Visual Basic .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express
- Swagger / OpenAPI

## Arquitectura del proyecto

El proyecto utiliza una arquitectura por capas:

- **Models:** entidades Categoria y Producto.
- **DTOs:** objetos para registrar, modificar y responder datos.
- **Data:** contexto de Entity Framework Core.
- **Repositories:** acceso a datos y operaciones con la base de datos.
- **Services:** lógica de negocio y validaciones.
- **Controllers:** endpoints REST para categorías y productos.
- **Middleware:** manejo global de excepciones.
- **Migrations:** migraciones de la base de datos.

## Modelo de datos

El sistema contiene dos entidades principales:

- **Categoria:** almacena el nombre y la descripción de cada categoría.
- **Producto:** almacena nombre, descripción, precio, stock, fecha de registro y categoría asociada.

La relación es de uno a muchos (1:N): una categoría puede tener varios productos y cada producto pertenece a una categoría.

## Funcionalidades

### Categorías

- Listar categorías.
- Consultar una categoría por ID.
- Registrar categorías.
- Modificar categorías.
- Eliminar categorías.
- Impedir nombres duplicados.
- Impedir eliminar categorías que tengan productos asociados.

### Productos

- Listar productos.
- Consultar un producto por ID.
- Consultar productos por categoría.
- Registrar productos.
- Modificar productos.
- Eliminar productos.
- Impedir nombres duplicados.
- Validar que la categoría exista.
- Validar que el precio sea positivo y el stock no sea negativo.

## Configuración de la base de datos

La aplicación utiliza SQL Server Express.

Configurar la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "ConexionSQL": "Server=localhost\\SQLEXPRESS;Database=InventarioDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Si el servidor SQL Server tiene otro nombre o instancia, modificar la cadena de conexión.

## Ejecución del proyecto

1. Abrir la solución en Visual Studio 2022.
2. Verificar la cadena de conexión a SQL Server.
3. Abrir la consola de administración de paquetes o una terminal en la carpeta del proyecto.
4. Ejecutar la actualización de la base de datos:

```bash
dotnet ef database update
```

5. Compilar y ejecutar el proyecto.
6. Abrir Swagger desde la dirección HTTPS que muestra Visual Studio.

## Códigos HTTP utilizados

| Código | Descripción |
|---|---|
| 200 | Operación realizada correctamente |
| 201 | Recurso creado |
| 204 | Recurso eliminado correctamente |
| 400 | Error de validación |
| 404 | Recurso no encontrado |
| 409 | Conflicto con una regla de negocio |
| 500 | Error interno del servidor |

## Pruebas

Las operaciones fueron verificadas mediante Swagger, incluyendo:

- Registro y consulta de productos y categorías.
- Modificación y eliminación de registros.
- Detección de nombres duplicados.
- Validación de stock y precio.
- Validación de categorías inexistentes.
- Restricción de eliminación de categorías con productos asociados.

## Autor

Proyecto académico desarrollado para la asignatura **Programación Orientada por Eventos Avanzada - Visual Basic**.