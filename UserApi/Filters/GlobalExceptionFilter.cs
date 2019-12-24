using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using UserApi.Exception;

namespace UserApi.Filters
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        ///  logger 日志注册进来。
        /// </summary>
        public readonly ILogger<GlobalExceptionFilter> _Logger;
        /// <summary>
        /// 当前的环境变量注册进来
        /// </summary>
        public readonly IHostingEnvironment _environment;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger,IHostingEnvironment environment)
        {
            _Logger = logger;
            _environment = environment;
        }


        public void OnException(ExceptionContext context)
        {
            var json=new JsonErrorRespone();

            //如果是开发环境就返回堆栈信息
            if (_environment.IsDevelopment())
                json.DevelopmentMessage = context.Exception.StackTrace;

            //用户类的异常自定义处理
            if (context.Exception.GetType() == typeof(UserOperationException))
            {
                json.Message = context.Exception.Message;
                context.Result=new BadRequestObjectResult(json);//这里示例返回的400错误回去
            }
            else
            {
                json.Message = "服务器内部发送未知异常";
                context.Result = new InternalServerExceptionObjectResult(json);
            }

            //写入log日志
            _Logger.LogError(context.Exception,"exceptionMessage");
        }
    }

    public class InternalServerExceptionObjectResult : ObjectResult
    {
        public InternalServerExceptionObjectResult(object error) : base(error)
        {
            //状态码返回服务器500错误
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
