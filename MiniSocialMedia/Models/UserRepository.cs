using Microsoft.EntityFrameworkCore;

namespace MiniSocialMedia.Models
{
    public static class UserRepository
    {
        public static List<User> Users { get; private set; } = new List<User>();

        public static bool AddUser(string login)
        {
            if (!string.IsNullOrWhiteSpace(login) && !IsUserExist(login))
            {
                Users.Add(new User(login));
                return true;
            }
            return false;
        }

        public static bool RemoveUser(string login)
        {
            var user = Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                Users.Remove(user);
                return true;
            }
            return false;
        }

        public static bool IsUserExist(string login)
        {
            if (Users.FirstOrDefault(u => u.Login == login) is not null) return true;
            return false;
        }


        public static User? GetUserByLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);
        }

        public static void InitializeSampleData()
        {
            for (int i = 0; i < 6; i++)
            {
                AddUser(RandomString(6));
            }
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
