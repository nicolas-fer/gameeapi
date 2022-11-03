using Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;

const string devCorsPolicy = "devCorsPolicy";
const string prdCorsPolicy = "prdCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});


var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSecret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      <br>
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br>
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddCommonServices(builder.Configuration);

builder.Services.AddMvc(o =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireRole("administrator")
        .Build();
    o.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, configurePolicy => {
        configurePolicy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy(prdCorsPolicy, configurePolicy => {
        configurePolicy.WithOrigins("https://nice-sand-0bfaa9c10.2.azurestaticapps.net", "https://gamee.softo.dev")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);
}
else
{
    app.UseCors(prdCorsPolicy);
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
