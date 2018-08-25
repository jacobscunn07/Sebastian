using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Infrastructure
{
    public class UserPrincipal
    {
        public UserPrincipal(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}