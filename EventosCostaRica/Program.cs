using CapaAccesoADatosDAL;
using CapaAccesoADatosDAL.Repositorios.Role;
using CapaAccesoADatosDAL.Repositorios.Usuario;
using CapaLogicaDeNegocioBLL.Servicios.Role;
using CapaLogicaDeNegocioBLL.Servicios.Usuario;
using CapaObjetos;
using CapaObjetos.ViewModelos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar MVC con filtro global de autenticación
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// 2. Configurar la cadena de conexión y DbContext
builder.Services.AddDbContext<EventoscostaricaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Inyección de dependencias para repositorios y servicios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRoleRepositorio, RoleRepositorio>();
builder.Services.AddScoped<IRoleService, RoleService>();

// 4. Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MapeoClases));

// 5. Registrar el hasher de contraseñas para UsuarioViewModelo
builder.Services.AddScoped<IPasswordHasher<UsuarioViewModelo>, PasswordHasher<UsuarioViewModelo>>();

// 6. Configurar autenticación por cookie
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// 7. Configurar autorización
builder.Services.AddAuthorization();

var app = builder.Build();

// 8. Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Orden correcto
app.UseAuthentication();
app.UseAuthorization();

// 9. Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
