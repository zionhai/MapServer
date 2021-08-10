using Services.Shared.Models;
using System.Collections.Generic;

namespace Services.Data
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}