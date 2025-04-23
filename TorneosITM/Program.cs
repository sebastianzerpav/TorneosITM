using TorneosITM.Data.Configuration;
using TorneosITM.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//ContextDb
DbConfiguration.Configuration(builder.Services, builder.Configuration);
//Services
ServicesConfiguration.Configuration(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
