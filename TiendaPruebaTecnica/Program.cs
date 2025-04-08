using Core.IRepositories;
using Core.IServices;
using Core.Mapper;
using Core.Repositories;
using Core.Services;
using Domain.Dto.DiscountProduct;
using Domain.Dto.Product;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext

builder.Services.AddDbContext<TiendaPtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaContext")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(CoreMapper));

//Independecyinjection 

builder.Services.AddScoped<IRepository<Product>, ProductoRepository>();
builder.Services.AddScoped<IRepository<DiscountProduct>, DiscountProductRepository>();


builder.Services.AddScoped<IcommonService<ProductDto, ProductInsertDto, ProductUpdateDto>, ProductService>();
builder.Services.AddScoped<IcommonService<DiscountProductDto, DiscountProductInsertDto, DiscountProductUpdateDto>, DiscountProductService>();

var app = builder.Build();

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
