using Infra.IoC;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddCommonServices(builder.Configuration);

builder.Services.AddMvc();

var devCorsPolicy = "devCorsPolicy";
var prdCorsPolicy = "prdCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy(prdCorsPolicy, builder => {
        builder.WithOrigins("https://nice-sand-0bfaa9c10.2.azurestaticapps.net", "https://gamee.softo.dev")
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

app.UseAuthorization();

app.MapControllers();

app.Run();
