using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static AgentMVCProject.Models.UserModel;

namespace AgentMVCProject.Controllers
{
    public class UserController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Users users = new Users();
        List<Users> _oUser = new List<Users>();

        public UserController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Route("get/user/all")]
        public async Task<List<Users>> GetAllUsers()
        {
            _oUser = new List<Users>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:25269/user"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUser = JsonConvert.DeserializeObject<List<Users>>(apiResponse);
                }
            }


            return _oUser;
        }
    }
}
