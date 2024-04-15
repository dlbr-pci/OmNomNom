using Microsoft.Extensions.Options;
using ServiceComposer.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:5173")
                   .AllowAnyHeader();
        }));
builder.Services.AddViewModelComposition();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.MapCompositionHandlers();

app.Run();