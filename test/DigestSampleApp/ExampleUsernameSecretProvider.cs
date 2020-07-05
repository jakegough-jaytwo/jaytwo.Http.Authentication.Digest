using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlakeyBit.DigestAuthentication.Implementation;

namespace DigestSampleApp
{
    internal class ExampleUsernameSecretProvider : IUsernameSecretProvider
    {
        public Task<string> GetSecretForUsernameAsync(string username)
        {
            if (username == "eddie")
            {
                return Task.FromResult("starwars123");
            }

            /// User not found
            return Task.FromResult<string>(null);
        }
    }

}
