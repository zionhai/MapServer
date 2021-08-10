using Services.Shared.Models;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthService
    {
        string LoginUser(LoginInfo loginInfo);
    }
}