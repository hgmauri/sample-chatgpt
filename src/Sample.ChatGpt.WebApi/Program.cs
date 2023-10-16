using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sample.ChatGpt.WebApi.Core.Extensions;
using Sample.ChatGpt.WebApi.Core.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog(builder.Configuration, "Sample ChatGPT");
builder.AddChatGpt(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddSwagger(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwaggerDoc();

app.MapControllers();

app.Run();