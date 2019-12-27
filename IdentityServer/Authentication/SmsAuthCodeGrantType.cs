using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer.Authentication
{
    /// <summary>
    /// 自定义验证
    /// </summary>
    public class SmsAuthCodeGrantType : IExtensionGrantValidator//继承至IdentityServer4.Validation下面的扩展验证方法
    {
        private readonly IUserService _userService;
        private readonly IAuthCodeService _authCodeService;

        public SmsAuthCodeGrantType(IUserService userService, IAuthCodeService authCodeService)
        {
            _userService = userService;
            _authCodeService = authCodeService;
        }

        /// <summary>
        /// 自定义名字后续会获取token时候会使用这里定义的名字
        /// </summary>
        public string GrantType => "sms_auth_code";

        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            //进行验证
            var phone = context.Request.Raw["phone"];
            var auth_code = context.Request.Raw["auth_code"];

            var errorResult=new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(auth_code))
            {
                context.Result = errorResult;
                return;
            }

            //验证手机验证码是否正确
            if (!_authCodeService.Validate(phone,auth_code))
            {
                context.Result = errorResult;
                return;
            }

            //验证用户是否正常返回
            var userId =await _userService.CheckOrCreate(phone);
            if (userId <= 0)
            {
                context.Result = errorResult;
                return;
            }
            context.Result=new GrantValidationResult(userId.ToString(),GrantType);
        }
    }
}
