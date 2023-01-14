using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InteractiveSpaces.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Microsoft.Extensions.Hosting;

string conexionURI = "ASPNETCORE_ENVIRONMENT";
string conexion = Environment.GetEnvironmentVariable(conexionURI);

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});


// Add services to the container.
// En caso de que el entorno de despliegue sea el de desarrollo todo funcionara como al comienzo 
/*
if (conexion == "Production")
{
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
    {

        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerDockerConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            //sqlOptions.MigrationsAssembly(
            //    typeof(Startup).GetTypeInfo()
            //    .Assembly.GetName().Name);

            //Configuring Connection Resiliency:
            sqlOptions.
                EnableRetryOnFailure(maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);

        });
    });
}
else
{
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQlServerLocalDB") ?? throw new InvalidOperationException("Connection string 'SQlServerLocalDB' not found.")));

}
*/

if(conexion != "Development")
{
    services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        Configuration.GetConnectionString("serviciotfg")));
}
else
{
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQlServerLocalDB") ?? throw new InvalidOperationException("Connection string 'SQlServerLocalDB' not found.")));

}



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Avoid problems while serializing
builder.Services.AddMvc().AddJsonOptions(o =>
{
    //o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

app.Logger.LogInformation("Generating intermediates");
app.MapGet("/", () => "Hello World!");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//It creates the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
   
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Starting the app");
app.Run();
