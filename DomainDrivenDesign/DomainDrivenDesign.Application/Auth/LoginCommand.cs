using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Application.Auth;
public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<string>>;

internal sealed class LoginCommandHandler(
    UserManager<User> userManager) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager
            .Users.Where(p => p.Email == request.EmailOrUserName
            || p.UserName == request.EmailOrUserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return Result<string>.Failure("User not found");
        }

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkPassword)
        {
            return Result<string>.Failure("Pasword is wrong");
        }

        string token = "token";
        return token;
    }
}
