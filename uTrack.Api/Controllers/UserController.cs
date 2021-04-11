using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using uTrack.Api.Model;
using uTrack.Business.Interface;
using uTrack.Data.Model;
using Nancy.Json;

namespace uTrack.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        
        [Route("current/{email}/{password}")]
        public async Task<ActionResult> GetCurrentUser(string email, string password)
        {
            List<UserModel> profile = new List<UserModel>();
            var user = await this._userService.GetUser(email, password);          
            if(user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            //profile.Add(user);
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //string uprofile = jss.Serialize(profile);
            return Ok(user);
        }
    }
}
