using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<CategoryApiService>(opt => opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));
builder.Services.AddHttpClient<ProductApiService>(opt =>opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));

builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

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

app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
