using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using UserApi.Controllers;
using UserApi.Data;
using UserApi.Entity.Model;
using Xunit;

namespace UserApiUnitTest
{
    public class UserControllerUnitTest
    {
        private UserContext GetUserContext()
        {
            //创建一个options
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())//设置内存数据库名字。
                .Options;

            //创建一个userContext
            var userContext=new UserContext(options);

            //初始化一个用户
            userContext.Users.Add(new User()
            {
                Id=2,
                Name="张三"
            });
            userContext.SaveChanges();
            return userContext;
        }

        private (UserController controller,UserContext userContext) GetUserController()
        {
            var userContext = GetUserContext();
            var controller = new UserController(userContext);

            //示例通过Moq框架来生成一个 iLogger
            var iLoggerMoq = new Mock<ILogger<UserController>>();
            var logger = iLoggerMoq.Object; //就自动生成了一个 ILogger<UserController> 的示例类

            return (controller,userContext); //使用 core新特性返回元祖。类似于net中的 ref
        }

        /// <summary>
        ///  测试user控制器中的get方法
        /// </summary>
        /// <returns></returns>
        [Fact] //表示这是一个测试方法
        public async Task Get_ReturnRightUser_WithParameters()
        {
            //使用新特性接收返回的2个参数
            var (controller, userContext) = GetUserController();
            //开始测试
            var response = await controller.Get();
            // 使用Assert验证返回的是不是 JsonResult 类型
            Assert.IsType<JsonResult>(response);
        }




        /// <summary>
        /// 使用FluentAPI 来替换 Assert 验证框架.
        /// 文档地址: https://fluentassertions.com/introduction
        /// 在实际开发中同一个方法需要写多个测试用例,每一种逻辑都需要写一个测试用例,
        /// 示例:string actual = "ABCDEFGHI"
        ///      actual.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9);
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Patch_ReturnNewName_WithNewNameParameters()
        {
            //使用新特性接收返回的2个参数
            var (controller, userContext) = GetUserController();

            //使用JsonPatchDocument拼接patch修改的参数
            var jsonPatch = new JsonPatchDocument<User>();
            jsonPatch.Replace(u => u.Name, "王五");
            var response = await controller.Patch(jsonPatch);

            //验证返回的类是不是 JsonResult
            var result =response.Should().BeOfType<JsonResult>().Subject;

            //把返回的数据转换成user类
            var user=result.Value.Should().BeAssignableTo<User>().Subject;

            //验证名字是否已经修改为王五
            user.Name.Should().Be("王五");

            //验证数据库中是否已经修改过来
            var baseUser=await userContext.Users.SingleOrDefaultAsync(l => l.Id == 2);
            
            //验证数据库中这个用户不能为null
            baseUser.Should().NotBeNull();

            //验证名字是否是王五
            baseUser.Name.Should().Be("王五");

        }

    }
}
