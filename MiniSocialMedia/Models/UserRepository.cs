namespace MiniSocialMedia.Models
{
    public static class UserRepository
    {
        public static List<User> Users { get; private set; } = new List<User>();

        public static bool AddUser(string login)
        {
            if (!string.IsNullOrWhiteSpace(login) && GetUserByLogin(login) is not null)
            {
                Users.Add(new User(login));
            }
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

        public static User? GetUserByLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);
        }

        public static void InitializeSampleData()
        {
            if (!Users.Any())
            {
                AddUser(new User("admin"));
                AddUser(new User("user1"));
                AddUser(new User("user2"));
                AddUser(new User("user3"));
                GetUserByLogin("user1")?.Friends.Add("user2");
                GetUserByLogin("user2")?.Friends.Add("user3");
            }
        }
    }
}
