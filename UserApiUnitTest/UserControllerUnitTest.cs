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
            //����һ��options
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())//�����ڴ����ݿ����֡�
                .Options;

            //����һ��userContext
            var userContext=new UserContext(options);

            //��ʼ��һ���û�
            userContext.Users.Add(new User()
            {
                Id=2,
                Name="����"
            });
            userContext.SaveChanges();
            return userContext;
        }

        private (UserController controller,UserContext userContext) GetUserController()
        {
            var userContext = GetUserContext();
            var controller = new UserController(userContext);

            //ʾ��ͨ��Moq���������һ�� iLogger
            var iLoggerMoq = new Mock<ILogger<UserController>>();
            var logger = iLoggerMoq.Object; //���Զ�������һ�� ILogger<UserController> ��ʾ����

            return (controller,userContext); //ʹ�� core�����Է���Ԫ�档������net�е� ref
        }

        /// <summary>
        ///  ����user�������е�get����
        /// </summary>
        /// <returns></returns>
        [Fact] //��ʾ����һ�����Է���
        public async Task Get_ReturnRightUser_WithParameters()
        {
            //ʹ�������Խ��շ��ص�2������
            var (controller, userContext) = GetUserController();
            //��ʼ����
            var response = await controller.Get();
            // ʹ��Assert��֤���ص��ǲ��� JsonResult ����
            Assert.IsType<JsonResult>(response);
        }




        /// <summary>
        /// ʹ��FluentAPI ���滻 Assert ��֤���.
        /// �ĵ���ַ: https://fluentassertions.com/introduction
        /// ��ʵ�ʿ�����ͬһ��������Ҫд�����������,ÿһ���߼�����Ҫдһ����������,
        /// ʾ��:string actual = "ABCDEFGHI"
        ///      actual.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9);
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Patch_ReturnNewName_WithNewNameParameters()
        {
            //ʹ�������Խ��շ��ص�2������
            var (controller, userContext) = GetUserController();

            //ʹ��JsonPatchDocumentƴ��patch�޸ĵĲ���
            var jsonPatch = new JsonPatchDocument<User>();
            jsonPatch.Replace(u => u.Name, "����");
            var response = await controller.Patch(jsonPatch);

            //��֤���ص����ǲ��� JsonResult
            var result =response.Should().BeOfType<JsonResult>().Subject;

            //�ѷ��ص�����ת����user��
            var user=result.Value.Should().BeAssignableTo<User>().Subject;

            //��֤�����Ƿ��Ѿ��޸�Ϊ����
            user.Name.Should().Be("����");

            //��֤���ݿ����Ƿ��Ѿ��޸Ĺ���
            var baseUser=await userContext.Users.SingleOrDefaultAsync(l => l.Id == 2);
            
            //��֤���ݿ�������û�����Ϊnull
            baseUser.Should().NotBeNull();

            //��֤�����Ƿ�������
            baseUser.Name.Should().Be("����");

        }

    }
}
