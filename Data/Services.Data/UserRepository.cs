using Newtonsoft.Json;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
    public class UserRepository : IUserRepository
    {

        public IEnumerable<User> GetUsers()
        {
            if (!File.Exists("Users.json")) return null;
            var usersJson = File.ReadAllText("Users.json");
            if (usersJson is null) return null;

            return JsonConvert.DeserializeObject<IEnumerable<User>>(usersJson);

        }
    }
}
