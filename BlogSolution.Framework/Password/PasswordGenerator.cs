using BlogSolution.Framework.Password.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogSolution.Framework.Password
{
    public sealed class PasswordGenerator
    {
        public UserPasswordModel _passwordOptions { get; private set; }
        public string Password { get; private set; }


        public PasswordGenerator(UserPasswordModel passwordOptions)
        {
            this._passwordOptions = passwordOptions;
            Password = Generate();
        }

        private string Generate()
        {
            if (_passwordOptions.IsRandomPassword)
            {
                if (_passwordOptions == null)
                    _passwordOptions = new UserPasswordModel
                    {
                        RequiredLength = 8,
                        RequiredUniqueChars = 4,
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireNonAlphanumeric = true,
                        RequireUppercase = true
                    };

                string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
                Random rand = new Random(Environment.TickCount);
                List<char> chars = new List<char>();

                if (_passwordOptions.RequireUppercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[0][rand.Next(0, randomChars[0].Length)]);

                if (_passwordOptions.RequireLowercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[1][rand.Next(0, randomChars[1].Length)]);

                if (_passwordOptions.RequireDigit)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[2][rand.Next(0, randomChars[2].Length)]);

                if (_passwordOptions.RequireNonAlphanumeric)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[3][rand.Next(0, randomChars[3].Length)]);

                for (int i = chars.Count; i < _passwordOptions.RequiredLength
                    || chars.Distinct().Count() < _passwordOptions.RequiredUniqueChars; i++)
                {
                    string rcs = randomChars[rand.Next(0, randomChars.Length)];
                    chars.Insert(rand.Next(0, chars.Count),
                        rcs[rand.Next(0, rcs.Length)]);
                }

                return new string(chars.ToArray());
            }
            else
            {
                return _passwordOptions.Password;
            }

        }
    }

}
