using ReactBackend.Interfaces;
using ReactBackend.Repositories;
using ReactBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Register the repository and its interface
builder.Services.AddScoped<UserInterface, UserRepository>();
// Register the file service and its interface
builder.Services.AddScoped<FileInterface, FileService>();
// Register the service
builder.Services.AddScoped<UserService>();

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

// Ensure CORS is applied before Routing and Authorization
app.UseCors("AllowReactApp");

app.UseRouting(); // Add UseRouting() explicitly

app.UseAuthorization();

app.MapControllers();

app.Run();
