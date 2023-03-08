using KaspelTestTask.Application;
using KaspelTestTask.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
#warning band-aid fix: get rid of circular reference by outputting cleaner result at GetFilteredOrders and CreateOrder
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// DateOnly as ISO 8601 string supports
builder.Services.AddDateOnlyTimeOnlyStringConverters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.UseDateOnlyTimeOnlyStringConverters());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
