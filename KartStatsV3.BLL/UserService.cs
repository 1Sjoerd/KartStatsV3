using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using System.Security.Cryptography;
using System.Text;

namespace KartStatsV3.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return false;
            }

            return user.PasswordHash == HashPassword(password);
        }

        public int? GetIdByUsername(string username)
        {
            var id = _userRepository.GetId(username);
            return id;
        }

        public string GetUsername()
        {
            var username = _userRepository.GetUsername();
            return username;
        }

        public void RegisterUser(User user, string password)
        {
            _userRepository.CreateUser(user, password);
        }

        public bool IsUserAdmin(Group group)
        {
            int? currentUserId = GetIdByUsername(GetUsername());

            if (group.AdminUserId == currentUserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
