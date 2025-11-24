using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using COMP306_Group_Project_Group_6.Models;
using COMP306_Group_Project_Group_6.Repositories;
using COMP306_Group_Project_Group_6.Scripts;

var builder = WebApplication.CreateBuilder(args);

// Get AWS credentials from appsettings.json
var awsRegion = builder.Configuration["AWS:Region"];
var awsAccessKey = builder.Configuration["AWS:AccessKey"];
var awsSecretKey = builder.Configuration["AWS:SecretKey"];

var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
var region = Amazon.RegionEndpoint.GetBySystemName(awsRegion);

// Add AWS DynamoDB
builder.Services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(credentials, region));
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

// Register repositories
builder.Services.AddScoped<IRepository<Airport>>(provider =>
    new DynamoDbRepository<Airport>(
        provider.GetRequiredService<IAmazonDynamoDB>(),
        "Airport"));

builder.Services.AddScoped<IRepository<Aircraft>>(provider =>
    new DynamoDbRepository<Aircraft>(
        provider.GetRequiredService<IAmazonDynamoDB>(),
        "Aircraft"));

builder.Services.AddScoped<IRepository<Flight>>(provider =>
    new DynamoDbRepository<Flight>(
        provider.GetRequiredService<IAmazonDynamoDB>(),
        "Flight"));

// Booking repositories
builder.Services.AddScoped<IRepository<Booking>>(provider =>
    new DynamoDbRepository<Booking>(
        provider.GetRequiredService<IAmazonDynamoDB>(),
        "Bookings"));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dynamoDb = services.GetRequiredService<IAmazonDynamoDB>();
        var context = new DynamoDBContext(dynamoDb);

        // Check if data already exists
        var existingAirports = await context.ScanAsync<Airport>(new List<ScanCondition>()).GetRemainingAsync();

        if (!existingAirports.Any())
        {
            Console.WriteLine("Database is empty. Seeding...");
            await SeedData.SeedDynamoDB(dynamoDb);
            Console.WriteLine("? Database seeded successfully!");
        }
        else
        {
            Console.WriteLine($"Database already contains {existingAirports.Count} airports. Skipping seed.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"? Error seeding database: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
