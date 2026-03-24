using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Application.Users.Commands.CreateUser;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Users;
using Shopaholics.Infrastructure.Persistance;
using Shopaholics.Infrastructure.Repositories;
using Shopaholics.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});

builder.Services.AddHttpClient<IProductService, ProductService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
