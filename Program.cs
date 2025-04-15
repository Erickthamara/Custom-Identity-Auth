using Custom_Identity_Auth.Controllers;
using Custom_Identity_Auth.Models;
using Microsoft.AspNetCore.Identity;
using Supabase;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load(); 
//Add env variables
//builder.Configuration.AddJsonFile($"appsettings.testEnv.json", optional: true);
string supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? throw new ArgumentNullException("Supabse URL missing!");
string supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY") ?? throw new ArgumentNullException("Supabse KEY missing!");

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IRoleStore<IdentityRole>, CustomRoleStore>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
builder.Services.AddTransient<IUserPasswordStore<ApplicationUser>, ApplicationUserStore>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
});


// Add services to the container.
builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(
    supabaseUrl,
    supabaseKey,
     new SupabaseOptions
     {
         AutoRefreshToken = true,
         AutoConnectRealtime = true,
     }
    ));

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


