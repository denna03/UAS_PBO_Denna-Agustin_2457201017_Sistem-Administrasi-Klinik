using ClinicAdminApp.Data;
using Microsoft.EntityFrameworkCore;
using ClinicAdminApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<DokterService>();
builder.Services.AddScoped<PasienService>();
builder.Services.AddScoped<PoliService>();
builder.Services.AddScoped<ObatService>();
builder.Services.AddScoped<PendaftaranService>();
builder.Services.AddScoped<PemeriksaanService>();
builder.Services.AddScoped<ResepService>();
builder.Services.AddScoped<PembayaranService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();