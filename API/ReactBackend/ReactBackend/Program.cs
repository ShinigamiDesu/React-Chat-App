using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReactBackend.Interfaces;
using ReactBackend.Repositories;
using ReactBackend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3003")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddScoped<UserInterface, UserRepository>();
builder.Services.AddScoped<UserFriendsInterface, UserFriendRepository>();
builder.Services.AddScoped<UserChatInterface, UserChatRepository>();
builder.Services.AddScoped<FileInterface, IMGService>();
// Register the service
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserChatService>();
builder.Services.AddScoped<IMGService>();
builder.Services.AddScoped<UserFriendService>();

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


