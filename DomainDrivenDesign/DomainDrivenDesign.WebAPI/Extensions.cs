using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.WebAPI;

public static class Extensions
{
    public static async Task CreateFirstAdminUser(WebApplication app)
    {
        var scoped = app.Services.CreateScope();
        var srv = scoped.ServiceProvider;

        var context = srv.GetRequiredService<ApplicationDbContext>();

        if (context.Users.Any(p => p.IsAdmin == new IsAdmin(true)))
        {
            FirstName firstName = new("Mahmut");
            LastName lastName = new("Demirkıran");
            UserName userName = new("mahmut");
            Email email = new("admin@gmail.com");
            User user = User.Create(firstName, lastName, userName, email);
            IsAdmin isAdmin = new(true);
            user.SetIsAdmin(isAdmin);

            var userManager = srv.GetRequiredService<UserManager<User>>();
            await userManager.CreateAsync(user, "Password12*");
        }
    }

    public static void DatabaseMigrate(WebApplication app)
    {
        var scoped = app.Services.CreateScope();
        var srv = scoped.ServiceProvider;

        var context = srv.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }

}
