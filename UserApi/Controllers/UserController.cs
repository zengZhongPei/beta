using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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

        /// <summary>
        /// 部分更新用户
        ///  参数传入文档:http://jsonpatch.com/
        /// </summary>
        /// <param name="patch">传入示例: [{ "op": "replace", "path": "/name", "value": "李四" },{"op": "replace", "path": "/Properties", "value": [{"Key":"userage","Value":"A+","Text":"A+"},{"Key":"userage","Value":"C+","Text":"C+"}]}]</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> Patch([FromBody]JsonPatchDocument<User> patch)
        {
            // 1.先引入Nuget包Microsoft.AspNetCore.JsonPatch
            // 2.参数接受JsonPatchDocument<实体名>
            // 3. 使用JsonPatch中的 ApplyTo 方法自动映射过去

            var user = await _userContext.Users
                .Include(u=>u.Properties)
                .SingleOrDefaultAsync(u => u.Id == userIdentity.UserId);
            if (user == null)
                throw new UserOperationException($"没有找到用户{userIdentity.UserId}");

            if (user.Properties.Any())
            {
                user.Properties.ForEach(u => { _userContext.Remove(u); });

                //由于实体类主键的关系需要先提交一次数据库在做新增。实际业务中可以做假删除避免多次提交
                await _userContext.SaveChangesAsync();
            }

            patch.ApplyTo(user);//自动把要修改的值映射到user中
            await _userContext.SaveChangesAsync();
            return Json(user);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var result = await _userContext.Users
                .AsNoTracking()
                .Include(l=>l.Properties)
                .SingleOrDefaultAsync(l=>l.Id == userIdentity.UserId);
            if (result == null)
                throw new UserOperationException($"没有找到用户{userIdentity.UserId}");
            return Json(result);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getuser")]
        public async Task<IActionResult> GetUser()
        {
            return Json(new{message= "这是CI持续集成:say hello" });
        }
    }
}
