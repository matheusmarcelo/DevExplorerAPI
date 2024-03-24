using DevExplorerAPI.DevExplorer.Repositories;
using DevExplorerAPI.DevExplorer.Interfaces;
using DevExplorerAPI.DevExplorer.Interfaces.IUser;
using DevExplorerAPI.DevExplorer.Repositories.UserRepository;
using DevExplorerAPI.DevExplorer.Services.UserService;
using DevExplorerAPI.DevExplorer.Interfaces.IUserService;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DevExplorerAPI.DevExplorer.Interfaces.IAuth;
using DevExplorerAPI.DevExplorer.Services.AuthService;
using DevExplorerAPI.DevExplorer.Repositories.AuthRepository;
using DevExplorerAPI.DevExplorer.Interfaces.ITask;
using DevExplorerAPI.DevExplorer.Repositories.TaskRepository;
using DevExplorerAPI.DevExplorer.Services.TaskService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JWT:issuer"],
            ValidAudience = builder.Configuration["JWT:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();