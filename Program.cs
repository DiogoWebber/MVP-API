using IntegraBrasilApi.Interfaces;
using IntegraBrasilApi.Mappings;
using IntegraBrasilApi.Rest;
using IntegraBrasilApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICepimService, CepimService>();
builder.Services.AddSingleton<IPepsService, PepsService>();

builder.Services.AddSingleton<IBrasilApi, BrasilApiRest>();
builder.Services.AddAutoMapper(typeof(CepimMapping));
builder.Services.AddAutoMapper(typeof(PepsMapping));



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
