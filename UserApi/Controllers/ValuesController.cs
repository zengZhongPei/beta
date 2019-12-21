using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Model;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly UserContext _userContext;
        public ValuesController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        [Route("addUser")]
        public async Task<IActionResult> AddUserAsync()
        {
            await _userContext.Users.AddAsync(new User() { Name="张三",Company = "HJ"});
            var result=await _userContext.SaveChangesAsync();
            return Json(result);
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> GetUserAsync()
        {
            var result = await _userContext.Users.SingleOrDefaultAsync(l=>l.Name=="张三");
            return Json(result);
        }
    }
}
