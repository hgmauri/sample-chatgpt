using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Sample.ChatGpt.WebApi.Core.Middleware;

public class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;

	public ErrorHandlingMiddleware(RequestDelegate next)
	{
		this._next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		Log.Error(exception, "Erro não tratado");

		var code = HttpStatusCode.InternalServerError;

		var result = JsonSerializer.Serialize(new
		{
			error = exception?.Message
		});

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;
		return context.Response.WriteAsync(result);
	}
}