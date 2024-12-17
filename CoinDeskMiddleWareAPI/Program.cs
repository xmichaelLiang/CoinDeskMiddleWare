using System.Security.Cryptography;
using System.Text;

using CoinDeskMiddleWareAPI.MiddlerWare;
using CoinDeskMiddleWareAPI.Repository;
using CoinDeskMiddleWareAPI.Service.BpiParser;
using CoinDeskMiddleWareAPI.Service.Currencys;
using CoinDeskMiddleWareAPI.Service.Factorys;
using CoinDeskMiddleWareAPI.Service.Strategies;
using CurrencyDBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Utility.EncryptUtility;
using Utility.HttpUtility;
using Utility.TokenUtility;

var builder = WebApplication.CreateBuilder(args);

// Load configuration based on the environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var configuration = builder.Configuration;
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinDesk API", Version = "v1" });
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"; c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    // Configure Swagger to use Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});


var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
var aesKeyString = configuration["AES:Key"];
var aesKey = Convert.FromBase64String(aesKeyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var encryptedToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var decryptedToken =AesEncryptionService.Decrypt(encryptedToken, aesKey);
            context.Token = decryptedToken;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddTransient<IBPIParserService,BPIParserService>();
builder.Services.AddTransient< ICurrencyDataService,CurrencyDataService>();
builder.Services.AddTransient<ICurrencyRepository,CurrencyRepository>();
builder.Services.AddTransient<ICoinDeskBPIQueryStrategyFactory,CoinDeskBPIQueryStrategyFactory>();
builder.Services.AddTransient<SpecificCurrencyCoinDeskStrategy>();
builder.Services.AddTransient<DefaultCoinDeskBPIQueryStrategy>();
builder.Services.AddTransient<ICurrencyChgLogsRepository,CurrencyChgLogsRepository>();
builder.Services.AddTransient<IHttpHelp,HttpHelp>();
builder.Services.AddTransient<ITokenHelp,TokenHelp>();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "zh-TW" };
    options.SetDefaultCulture(supportedCultures[1])
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});
var app = builder.Build();
// Ensure it listens on 0.0.0.0
if (builder.Environment.IsEnvironment("Docker"))
{
    app.Urls.Add("http://0.0.0.0:80");
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
if (configuration["MigrationsDB"] == "Y") {
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();
        dbContext.Database.Migrate();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
