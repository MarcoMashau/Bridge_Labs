using BridgeLabsShop.Api.Data;
using BridgeLabsShop.Api.Repositories;
using BridgeLabsShop.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//this here is our products db context and we're registering it for dependency injection
builder.Services.AddDbContext<BridgeLabsShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//registering our product repository class so we can use it for dep injection 

builder.Services.AddScoped<IProductRepository,ProductRepository >();



//addscoped - the same instance of an object is injected to the relevant classes within a paricular  http reqeust 
//addTransient - a new intance of the object is provided to every class that requires the object to be injected 
//addsingleton -  the same instance of an object is injected to the relevant classes for every http request 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //this is an inteface description language for descibing restful api's , expressed using JSON, We can use this to test our action methods.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
