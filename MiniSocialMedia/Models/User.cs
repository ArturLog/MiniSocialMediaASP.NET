using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MiniSocialMedia.Models
{
    public class User
    {
        [HiddenInput]
        [Key]
        public Guid Uuid { get; set; }
        public string Login { get; set; } 
        public DateTime CreatedAt { get; set; }
        public List<User> Friends { get; set; }
    }
}
  