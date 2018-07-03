using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;
using StagingWizard.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace StagingWizard.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        public IUserRepository UserRepository { get; }

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [Route("signin")]
        [HttpPost]
        public User SignIn([FromBody]User user)
        {
            return UserRepository.SignInCheck(user);
        }

        [Route("registrate")]
        [HttpPost]
        public User AddUser([FromBody]User user)
        {
            return UserRepository.AddUser(user);
        }
    }
}