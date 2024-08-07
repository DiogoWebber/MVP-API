using Microsoft.EntityFrameworkCore;
using mvpAPI.Interfaces;
using mvpAPI.Mappings;
using mvpAPI.Rest;
using mvpAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuração do Entity Framework Core para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICepimService, CepimService>();
builder.Services.AddSingleton<IPepsService, PepsService>();

builder.Services.AddSingleton<IMvpApi, MvpApiRest>();
builder.Services.AddAutoMapper(typeof(CepimMapping));
builder.Services.AddAutoMapper(typeof(PepsMapping));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();