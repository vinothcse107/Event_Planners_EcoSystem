
using System.Text.Json.Serialization;
using API.ModelValidation;
using Newtonsoft.Json.Serialization;

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

app.UseCors(x => x.AllowAnyHeader().WithMethods().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
