using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApi.Entity.Dtos;

namespace UserApi.Controllers
{
    public class BaseController:Controller
    {
        protected UserIdentity userIdentity => new UserIdentity() { UserId=4,Name = "张三"};
    }
}
