using System.Collections.Generic;

namespace UserApi.Entity.Model
{
    public class User
    {

        public User()
        {
            //初始化一下属性避免从数据库里面出来直接是null
            Properties=new List<UserProperty>();
        }

        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

       /// <summary>
       /// 职位
       /// </summary>
        public string Title { get; set; }

       /// <summary>
       /// 手机号码
       /// </summary>
       public string Phone { get; set; }

       /// <summary>
       /// 头像地址
       /// </summary>
       public string Avatar { get; set; }

       /// <summary>
       /// 性别 1:男  2:女
       /// </summary>
       public byte Gender { get; set; }
        
       /// <summary>
        /// 详细地址
        /// </summary>
       public string Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
       public string Email { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; }


        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 名片地址
        /// </summary>
        public string NameCard { get; set; }

        /// <summary>
        /// 用户属性列表
        ///  注意事项；如果查询需要用include。子类里面需要包含 "类名+Id" 属性字段则使用include会自动映射
        /// </summary>
        public List<UserProperty> Properties { get; set; }
    }
}
