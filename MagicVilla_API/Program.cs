using MagicVilla_API;
using MagicVilla_API.Datos;
using MagicVilla_API.Repository;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregando Servicios
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingConfig));

//Se crea una vez y se destruye
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
//Se crea cada solicitud porterior utilizara la misma instancia
//builder.Services.AddSingleton
//Transitorios se crean cada vez que se utilizan,sin estado,servicios livianos 
//builder.Services.AddTransient
builder.Services.AddScoped<INumeroVillaRepository, NumeroVillaRepository>();
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
