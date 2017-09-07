using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    public class UserJoin
    {
        public string userName { get; set; }
        public DateTime timestamp { get; set; }
    }
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public ulong DiscordUserId { get; set; }
        public string APIKey { get; set; }

        public User()
        {
            UserId = Guid.NewGuid();
        }
    }
}
