using Microsoft.EntityFrameworkCore;
using tnsvideos.Data;
using tnsvideos.Repository;
using tnsvideos.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registra tu servicio
builder.Services.AddScoped<VideoServices>();
builder.Services.AddScoped<VideosRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();