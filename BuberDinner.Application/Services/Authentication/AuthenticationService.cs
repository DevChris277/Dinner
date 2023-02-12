using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;


namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Check if user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // Check if the password matches
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // Generate token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Create authentication result
        return new AuthenticationResult(
            user,
            token
        );
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {

        // ? Does User exist?
        // Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        // Save user to repository
        _userRepository.Add(user);

        // Generate token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Create authentication result 
        return new AuthenticationResult(
            user,
            token
        );
    }

}