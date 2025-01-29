using Enilton.Application.Handlers.Customer;
using Enilton.Domain.Interfaces;
using Enilton.Persistence;
using Enilton.Persistence.Context;
using Enilton.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("SqlConnection");
    options.UseSqlServer(connectionString);
    var databaseInitializer = new DatabaseInitializer(connectionString);
    databaseInitializer.InitializeDatabase();
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("NoSqlConnection");
    var databaseName = configuration["NoSql:DatabaseName"];
    return new MongoDbContext(connectionString, databaseName);
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

builder.Services.AddMediatR(typeof(CreateCustomerCommandHandler).GetTypeInfo().Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
});

var app = builder.Build();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();