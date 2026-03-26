using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Application.Users.Commands.CreateUser;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Users;
using Shopaholics.Infrastructure.Persistance;
using Shopaholics.Infrastructure.Repositories;
using Shopaholics.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("https://localhost:54669")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

//DB
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite("Data Source=shopaholics.db"));

builder.Services.AddMemoryCache();

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});

builder.Services.AddHttpClient<IProductService, ProductService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    db.Database.EnsureCreated();
}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAngular");

app.MapFallbackToFile("/index.html");

app.Run();
