using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User GetUserById(int id);
        string GetUsername();
        int? GetId(string username);
        void CreateUser(User user, string password);
    }
}
