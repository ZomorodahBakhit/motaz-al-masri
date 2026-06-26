using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoWrapper;
using Microsoft.EntityFrameworkCore;
using University.API.Modules;
using University.Data;

var builder = WebApplication.CreateBuilder(args);

// إعدادات Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepositoryModule());
    containerBuilder.RegisterModule(new ServiceModule());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// إبقاء إعدادات Swagger فقط وإزالة AddOpenApi
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<universityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "University API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

//app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
//{
//IsApiOnly = false,
//  BypassHTMLValidation = true
//});

app.MapControllers();

app.Run();