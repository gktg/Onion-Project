using Microsoft.EntityFrameworkCore;
using OnionProject.Domain.Entities;
using OnionProject.Persistance;
using OnionProject.Persistance.Contexts;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        OnionProjectDbContext dbContext = scope.ServiceProvider.GetRequiredService<OnionProjectDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        Data(dbContext);
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void Data(OnionProjectDbContext context)
{
    var customer = new Customer()
    {
        Email = "g@mail.com",
        City = "Ýstanbul",
        Address = "Kuba Cafe üstü",
        ApartmentNumber = "7",
        HomeNumber = "3",
        IsActive = true,
        Name = "Göktuð",
        Neighbourhood = "Fener yolu",
        Password = "n0KUSA+XaT6wK9rJXon1s2T4T3Ee67+zCdFaVOvyL9k=",
        Phone = "05388828249",
        Street = "Fener yolu",
        Surname = "Türkan",
    };

    context.Customers.Add(customer);
    context.SaveChanges();
    
}