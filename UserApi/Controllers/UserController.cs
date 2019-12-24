using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Entity.Model;
using UserApi.Exception;

namespace UserApi.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly UserContext _userContext;
        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> AddUserAsync()
        {
            await _userContext.Users.AddAsync(new User() { Name="张三",Company = "HJ"});
            var result=await _userContext.SaveChangesAsync();
            return Json(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var result = await _userContext.Users
                .AsNoTracking()
                .Include(l=>l.Properties)
                .SingleOrDefaultAsync(l=>l.Id == userIdentity.UserId);
            if (result == null)
                throw new System.Exception($"没有找到用户{userIdentity.UserId}");
            return Json(result);
        }
    }
}
