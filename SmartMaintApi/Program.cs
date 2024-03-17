using Microsoft.EntityFrameworkCore;
using SmartMaintApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApiDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
    // options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_Connection")));

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

