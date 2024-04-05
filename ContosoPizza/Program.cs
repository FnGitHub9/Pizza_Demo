using ContosoPizza.DataContexts;
using ContosoPizza.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
/*builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlite("Data Source=ContosoPizza.db"));*/

builder.Services.AddScoped<PizzaService>();

builder.Services.AddScoped<PizzaContext>(provider =>
{
    string filePath = "CSV-Files/ContosoPizza.csv";

    return new PizzaContext(filePath);
}
);

builder.Services.AddScoped<DrinkService>();

builder.Services.AddScoped<DrinkContext>(provider =>
{
    string filePath = "CSV-Files/Drinks.csv";

    return new DrinkContext(filePath);
}
);

var app = builder.Build();
//This is a third test

//This is a forth test

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
