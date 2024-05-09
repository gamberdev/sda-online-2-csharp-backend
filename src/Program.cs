using System.Text;
using System.Text.Json.Serialization;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]!);
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



// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://example.com")); // Specify allowed origins
});



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
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

builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddAutoMapper(typeof(Program));

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




// Exception handling middleware
app.UseExceptionHandler("/error"); // Specify a custom error handling endpoint

// Security headers
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers.Append("Content-Security-Policy", "...");
    headers.Append("X-Content-Type-Options", "nosniff");
    headers.Append("X-Frame-Options", "DENY");
    await next();
});


// Health checks
app.UseHealthChecks("/health");

// Use authentication
app.UseAuthentication();



app.MapControllers().WithParameterValidation();
app.UseHttpsRedirection();

// Run the application
app.Run();



// Use WithOrigins for CORS: If you need to enable CORS (Cross-Origin Resource Sharing), you can use the WithOrigins method to specify the allowed origins. This helps enhance security by restricting which origins can access your API.

// Logging Configuration: Configure logging settings, especially in production environments, to capture errors and important information. This can be achieved by adding logging middleware and configuring log providers like Serilog or the built-in logging framework.

// Exception Handling Middleware: Implement global exception handling middleware to catch unhandled exceptions and return appropriate error responses to clients. This ensures consistent error handling across the application.

// Security Headers: Add security headers to enhance the security of your application. Headers like Content-Security-Policy, X-Content-Type-Options, and X-Frame-Options can help protect against common web vulnerabilities.

// Health Checks: Implement health checks to monitor the health of your application. This can be useful for automated monitoring and alerting systems.