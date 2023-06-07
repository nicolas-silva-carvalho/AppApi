using System.Text;
using System.Text.Json.Serialization;
using ApiAPP.Data;
using ApiAPP.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using AutoMapper;
using AppApi.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using ApiAPP.Data.Map;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["jwt:Issuer"],
            ValidAudience = builder.Configuration["jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]!))
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors();
builder.Services.Configure<ApiAppDatabase>(builder.Configuration.GetSection("RelatorioDatabase"));

builder.Services.AddSingleton<ApiAppDatabase>();
builder.Services.AddSingleton<ConexaoBancoPrefeitura>();

builder.Services.AddScoped<ProfileAPP>();
builder.Services.AddSingleton<AppService>();
builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IToken, TokenService>();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
        {
            Description = @"JWT Authorization header usando Bearer.
                            Entre com o 'Bearer' [espaço] então coloque seu token.
                            Exemplo: 'Bearer 123abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement() 
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>() 
            }
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(acesso => { acesso.AllowAnyHeader(); acesso.AllowAnyMethod(); acesso.AllowAnyOrigin(); });
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
