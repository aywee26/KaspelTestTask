using KaspelTestTask.Application;
using KaspelTestTask.Infrastructure;
using KaspelTestTask.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// DateOnly as ISO 8601 string support
builder.Services.AddDateOnlyTimeOnlyStringConverters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.UseDateOnlyTimeOnlyStringConverters());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.PrepareDatabase();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
