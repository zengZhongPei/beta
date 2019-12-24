using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Exception
{
    /// <summary>
    /// 自定义用户异常类型
    /// </summary>
    public class UserOperationException:System.Exception
    {
        //直接使用base类的构造函数

        public UserOperationException() { }

        public UserOperationException(string message) : base(message)
        {

        }

        public UserOperationException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
