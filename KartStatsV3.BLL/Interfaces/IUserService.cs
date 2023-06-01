using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IUserService
    {
        bool Authenticate(string username, string password);
        int? GetIdByUsername(string username);
        string GetUsername();
        void RegisterUser(User user, string password);
        bool IsUserAdmin(Group group);
    }
}
