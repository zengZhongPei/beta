using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    /// <summary>
    /// 用户验证服务
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 根据手机号返回一个用户ID
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<int> CheckOrCreate(string phone);
    }
}
