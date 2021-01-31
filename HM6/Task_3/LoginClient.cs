using System;
using System.Threading;

namespace Task_3
{
    internal static class LoginClient
    {
        internal static string Login(string login, string password)
        {
            login.Equals(password);

            var rnd = new Random();
            Thread.Sleep((int)(1000 * rnd.NextDouble()));
            if (rnd.NextDouble() >= 0.5)
            {
                return Guid.NewGuid().ToString();
            }   //true
            else
            {
                return null;
            }   //false

        }
    }
}
