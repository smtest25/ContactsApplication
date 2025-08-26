using Microsoft.EntityFrameworkCore;
using ContactsApplication.Data;
using ContactsApplication.Repositories;
using ContactsApplication.Resources;
using ContactsApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(GlobalResource));
    });

builder.Services.AddDbContext<ContactContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IContactService, ContactService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ContactContext>().Initialize();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
