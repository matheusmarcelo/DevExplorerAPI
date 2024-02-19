using DevExplorerAPI.DevExplorer.Repositories;
using DevExplorerAPI.DevExplorer.Interfaces;
using DevExplorerAPI.DevExplorer.Interfaces.IUser;
using DevExplorerAPI.DevExplorer.Repositories.UserRepository;
using DevExplorerAPI.DevExplorer.Services.UserService;
using DevExplorerAPI.DevExplorer.Interfaces.IUserService;
using DevExplorerAPI.DevExplorer.Models.DapperContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

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