using DaData.Application.ApplicationDependencies;
using DaData.Infrastructure.InfrastructureDependencies;
using DaData.Infrastructure.Services.DaData;
using DaData.Presentation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.Configure<DaDatakeys>(configuration.GetSection("DaData"));
builder.Services.AddPresentationDependencies();
builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructureDependencies();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.UseAuthorization();

app.Run();
