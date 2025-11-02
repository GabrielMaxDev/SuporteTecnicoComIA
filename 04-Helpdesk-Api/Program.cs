using Helpdesk.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- ADICIONE AS LINHAS DE LOG AQUI ---
builder.Logging.ClearProviders(); // Opcional, mas limpa provedores padrão
builder.Logging.AddConsole();     // Adiciona o log ao console preto
builder.Logging.AddDebug();       // Adiciona o log à janela "Saída" do VS
// ------------------------------------


// --- 1. Definir a política de CORS ---
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

// --- 2. Registrar o "Motor" do Banco de Dados (DbContext) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HelpdeskDBContext>(options =>
    options.UseSqlServer(connectionString)
);

// Adiciona suporte para os Controllers (como o AuthController)
builder.Services.AddControllers();

var app = builder.Build();

// --- 3. Habilitar o CORS ---
app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseAuthorization();

// Diz à API para encontrar e usar os Controllers
app.MapControllers();

app.Run();