using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MiniSocialMedia.Models
{
    public class User(string login)
    {
        [HiddenInput]
        [Key]
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = login;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<User> Friends { get; set; } = new List<User>();
    }
}
  