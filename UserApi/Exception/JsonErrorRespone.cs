using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Exception
{
    /// <summary>
    /// 自定义异常返回类
    /// </summary>
    public class JsonErrorRespone
    {
        /// <summary>
        /// 异常消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 开发环境的异常信息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }
}
