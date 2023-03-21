using CartingService.Application.Common.Interfaces;

namespace CartingService.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
