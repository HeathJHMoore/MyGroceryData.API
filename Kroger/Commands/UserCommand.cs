using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kroger.Commands
{
    public class UserCommand
    {
        public string FirebaseId { get; set; }
        public string DefaultLocationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
