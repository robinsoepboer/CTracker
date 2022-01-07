using CTracker.DAL;
using CTracker.DAL.Entities;
using CTracker.Repositories;
using CTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<CTrackerContext>(options => options.UseSqlServer("name=ConnectionStrings:Default"));

builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IBitvavoService, BitvavoService>();
builder.Services.AddScoped<ISignatureService, SignatureService>();
builder.Services.AddScoped<ITradeService, TradeService>();

builder.Services.AddScoped<IRepository<Portfolio>, Repository<Portfolio>>();
builder.Services.AddScoped<IRepository<Coin>, Repository<Coin>>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IRepository<Asset>, Repository<Asset>>();
builder.Services.AddScoped<IRepository<AssetHistory>, Repository<AssetHistory>>();

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

