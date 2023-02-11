namespace BuberDinner.Contracts.Authentication;

 public record AuthenticationResponce(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
 );