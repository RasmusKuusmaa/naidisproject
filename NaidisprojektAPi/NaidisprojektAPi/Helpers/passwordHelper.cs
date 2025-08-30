using System.Security.Cryptography;
using System.Text;

namespace NaidisprojektAPi.Helpers
{
    public static class passwordHelper
    {
        public static byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));    
            }
        }
    }
}
