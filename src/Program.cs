using System.Text;
using System.Text.Json.Serialization;
using ecommerce.EntityFramework;
using ecommerce.Middleware;
using ecommerce.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
// Get JWT settings from environment variables


var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key") ?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");

var DefaultConnection = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException("Default Connection is missing in environment variables.");
var Configuration = builder.Configuration;

var key = Encoding.ASCII.GetBytes(jwtKey);
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // set this one as a true in production
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

// claim-based for banned user
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiredNotBanned", policy => policy.RequireClaim("IsBanned", "false"));
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(DefaultConnection)
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Backend E-commerce system API",
            Description =
                "Experience the future of online shopping today with E-commerce API. Designed to provide services for the system's users to manage products, and categories and engage customers with good experience. The goal is to deliver the tools needed to elevate a good e-commerce platform and drive incomparable success.",
            Version = "v1"
        }
    );
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();// token
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

}

app.MapControllers().WithParameterValidation();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowSpecificOrigin");
// Run the application
app.Run();
