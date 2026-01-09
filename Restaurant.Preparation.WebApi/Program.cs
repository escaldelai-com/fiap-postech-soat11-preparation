using Restaurant.Preparation.Application;
using Restaurant.Preparation.Data;
using Restaurant.Preparation.ExternalServices;
using Restaurant.Preparation.Facade;
using Restaurant.Preparation.Presenter;
using Restaurant.Preparation.WebApi.Middleware;
using Restaurant.Preparation.WebApi;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services
    .AddPresenter()
    .AddData()
    .AddExternalServices()
    .AddApplication()
    .AddFacade()
    .AddAuthentication(builder.Configuration)
    .AddRestaurantAuthorization()
    .AddOpenApi();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandler();
app.UseErrorHandler();
app.MapControllers();


// Run the application
app.Run();
