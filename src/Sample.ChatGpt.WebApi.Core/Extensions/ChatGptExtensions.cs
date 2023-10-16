using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI_API;

namespace Sample.ChatGpt.WebApi.Core.Extensions;

public static class ChatGptExtensions
{
	public static WebApplicationBuilder AddChatGpt(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		var key = configuration["ChatGpt:Key"];

		var chat = new OpenAIAPI(key);

		builder.Services.AddSingleton(chat);

		return builder;
	}
}