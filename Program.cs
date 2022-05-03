using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using CentRent.Helpers;
using CentRent.Services;
using CentRent.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    var jsonInputFormatter = options.InputFormatters.OfType<SystemTextJsonInputFormatter>().First();
    jsonInputFormatter.SupportedMediaTypes.Add("multipart/form-data");
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();


builder.Services.Configure<ApiBehaviorOptions>(opt => {
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlite<CentRentContext>("Data Source=CentRent.db");
builder.Services.AddScoped<CentRentContext>();

builder.Services.AddCors(c =>  
{  
    c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader() );
     
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.CreateDbIfNotExists();

app.Run();
