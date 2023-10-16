using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace Sample.ChatGpt.WebApi.Core.Extensions;

public static class SerilogExtensions
{
	public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string applicationName)
	{
		Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).Enrich.FromLogContext().Enrich.WithMachineName().Enrich.WithEnvironmentUserName().Enrich
			.WithExceptionDetails().Enrich.WithProperty("ApplicationName", $"{applicationName} - {configuration.GetSection("DOTNET_ENVIRONMENT")?.Value}").WriteTo
			.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")).CreateLogger();

		builder.Logging.ClearProviders();
		builder.Host.UseSerilog(Log.Logger, true);

		return builder;
	}
}