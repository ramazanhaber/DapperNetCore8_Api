using DapperNetCore8_Api.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IO.Compression;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*
 KURULMASI GEREKEN NUGETLER
 
 Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.1
 Install-Package Dapper.Contrib -Version 2.0.78
 Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.1
 Install-Package Newtonsoft.Json -Version 13.0.3
 */

string defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

IDbConnection defaultConnection = new SqlConnection(defaultConnectionString);


string secondConnectionString = builder.Configuration.GetConnectionString("SecondConnection");

IDbConnection secondConnection = new SqlConnection(secondConnectionString);



builder.Services.AddSingleton(new DatabaseConnections(defaultConnection, secondConnection));


builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"))
        };
    });

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest; // Sýkýþtýrma seviyesi
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>(); // gzip sýkýþtýrma saðlayýcýsýný ekler
});


var app = builder.Build();

app.UseResponseCompression();


app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()
);

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "myapi v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();// auth için
app.UseAuthorization();
app.MapControllers();
app.Run();
