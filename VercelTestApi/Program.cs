using Microsoft.EntityFrameworkCore;
using VercelTestApi.Models.DataContext;
using VercelTestApi.Models.Mapping;
using VercelTestApi.Repository.CategoryRepository;
using VercelTestApi.Repository.GenericRepository;
using VercelTestApi.Repository.ProductRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VercelDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VercelConnectionString"));
});

// Register Repository in Middleware
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
