Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Text.Json
Imports Microsoft.AspNetCore.Http
Imports Microsoft.Extensions.Logging

Namespace Middleware

    Public Class ExceptionMiddleware

        Private ReadOnly _next As RequestDelegate
        Private ReadOnly _logger As ILogger(Of ExceptionMiddleware)

        Public Sub New(nextDelegate As RequestDelegate,
                       logger As ILogger(Of ExceptionMiddleware))

            _next = nextDelegate
            _logger = logger

        End Sub

        Public Async Function InvokeAsync(context As HttpContext) As Task
            Await Ejecutar(context)
        End Function



        Private Async Function Ejecutar(context As HttpContext) As Task

            Dim statusCode As Integer = 500
            Dim mensaje As String = "Ocurrió un error interno en el servidor."
            Dim ocurrioError As Boolean = False

            Try
                Await _next(context)

            Catch ex As Exception

                _logger.LogError(ex, "Ocurrió un error al procesar la solicitud.")

                If context.Response.HasStarted Then
                    Throw
                End If

                ocurrioError = True

                If TypeOf ex Is ArgumentException Then
                    statusCode = 400
                    mensaje = ex.Message

                ElseIf TypeOf ex Is KeyNotFoundException Then
                    statusCode = 404
                    mensaje = ex.Message

                ElseIf TypeOf ex Is InvalidOperationException Then
                    statusCode = 409
                    mensaje = ex.Message
                End If

            End Try

            If ocurrioError Then

                context.Response.Clear()
                context.Response.StatusCode = statusCode
                context.Response.ContentType = "application/json"

                Dim respuesta = New With {
            .status = statusCode,
            .mensaje = mensaje
        }

                Dim json = JsonSerializer.Serialize(respuesta)

                Await context.Response.WriteAsync(json)

            End If

        End Function



    End Class

End Namespace
