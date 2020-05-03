using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleNetCoreAPI.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; internal set; }
    }
}
