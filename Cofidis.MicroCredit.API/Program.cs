using Cofidis.MicroCredit.Data;
using Cofidis.MicroCredit.Data.Interfaces;
using Cofidis.MicroCredit.Data.Repository;
using Cofidis.MicroCredit.Manager.Interfaces;
using Cofidis.MicroCredit.Manager.Management;
using Cofidis.MicroCredit.Manager.Mapping;
using Cofidis.MicroCredit.Services.Interfaces;
using Cofidis.MicroCredit.Services.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();



//Repos
builder.Services.AddScoped<IMicroCreditRepository,MicroCreditRepository>();

//Services
builder.Services.AddScoped<IMicroCreditService, MicroCreditService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IMicroCreditValidatorService, MicroCreditValidatorService>();


//Mngrs
builder.Services.AddScoped<IMicroCreditManager, MicroCreditManager>();
builder.Services.AddScoped<IHttpClientManager, HttpClientManager>();

//Mps
builder.Services.AddAutoMapper(typeof(ClientUserMappingProfile));

//External API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000/")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// DB Configs
var database = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(database);
});



var app = builder.Build();
// App Cors
app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
  .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
