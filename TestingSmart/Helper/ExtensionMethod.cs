using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingSmart.Models;

namespace TestingSmart.Helper
{
    public static class ExtensionMethod
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.PasswordHash = null;
            return user;
        }
    }
}
