using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Infrastructure
{
    public interface IUserPrincipal
    {
        User User { get; set; }
    }
}
