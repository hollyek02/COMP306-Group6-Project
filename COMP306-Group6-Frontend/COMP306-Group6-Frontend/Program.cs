var builder = WebApplication.CreateBuilder(args);

var backendUrl = builder.Configuration["BackendApi:BaseUrl"];
builder.Services.AddHttpClient("FlightApi", client =>
{
    client.BaseAddress = new Uri(backendUrl);
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AircraftService>();
builder.Services.AddScoped<AirportService>();
builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<BookingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
