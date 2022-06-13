
using System.Text.Json.Serialization;
using API.ModelValidation;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// => Extensions

builder.Services.BuilderServices(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
      var context = services.GetRequiredService<Context>();
      context.Database.Migrate();
      var s = new Seed(context);
}
catch (Exception ex)
{
      var logger = services.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "An error occurred during migration");
}
app.Run(async (context) =>
{
      await Task.Run(() =>
      {
            context.Response.Redirect("https://localhost:5001/swagger/index.html");
      });
});
app.Run();


