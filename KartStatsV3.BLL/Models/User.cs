using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }

        public User(int id, string username, string passwordHash, string email)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }

        public User(int id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public User(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
