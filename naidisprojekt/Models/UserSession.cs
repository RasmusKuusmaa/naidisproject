using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naidisprojekt.Models
{
    public class UserSession
    {
        private static UserSession _instance;
        public static UserSession Instance => _instance ??= new UserSession();  

        public int? UserId { get; private set; }
        public void SetUserId(int? userId)
        {
            UserId = userId;
        }
        public void Clear()
        {
            UserId = null;
        }

    }
}
