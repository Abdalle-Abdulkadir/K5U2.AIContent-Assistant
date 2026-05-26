using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using ServiceA.ContentApi.Clients;
using ServiceA.ContentApi.Data;
using ServiceA.ContentApi.Filters;
using ServiceA.ContentApI.Repositorries;
using ServiceA.ContentApI.Services;
using System.Threading.RateLimiting;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;

    options.AddSlidingWindowLimiter("ai-limit", limiterOptions =>
    {
        limiterOptions.PermitLimit = 1;
        limiterOptions.Window = TimeSpan.FromSeconds(30);
        limiterOptions.SegmentsPerWindow = 6;
        limiterOptions.QueueLimit = 0;
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EmailAssistantDb"));

builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddHttpClient<LlmProxyClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<IEmailService, EmailService>();




var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {         
        options.OpenApiRoutePattern = "/swagger/v1/swagger.json";
    });

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();

