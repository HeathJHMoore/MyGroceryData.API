using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kroger.Commands;
using Kroger.Repositories;
using Kroger.Models;

namespace Kroger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Check to see if a user is in database using firebaseId
        [HttpGet("checkuser/{firebaseId}")]
        public int CheckUser(string firebaseId)
        {
            var repo = new UserRepository();
            var check = repo.CheckUser(firebaseId);
            return check;
        }

        // Create user account in database
        [HttpPost("createuser/{firebaseId}")]
        public IActionResult CreateUser(UserCommand usercommand)
        {
            var repo = new UserRepository();
            repo.CreateUser(usercommand);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
