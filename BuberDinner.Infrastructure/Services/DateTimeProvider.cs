using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}