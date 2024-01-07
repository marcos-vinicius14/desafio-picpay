using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Picpay_01.Data;
using Picpay_01.Services;


var builder = WebApplication.CreateBuilder(args);

ConfigureMVC(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();


void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services
        .AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

        });
}

void ConfigureServices(WebApplicationBuilder builder)
{
     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
     builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

     builder.Services.AddScoped<TransactionService>();
     builder.Services.AddTransient<NotificationService>();
     builder.Services.AddTransient<UserService>();
     
     builder.Services.AddHttpClient();
 }
