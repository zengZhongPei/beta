using Microsoft.EntityFrameworkCore;
using UserApi.Entity.Model;

namespace UserApi.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //注意: mysql主键长度不能超过255个字段超过后会有意外的bug

            modelBuilder.Entity<User>()
                .ToTable("Users")//设置表名
                .HasKey(u=>u.Id);//设置主键

            modelBuilder.Entity<UserTag>()
                .ToTable("UserTags")
                .HasKey(u => new{u.UserId,u.Tag})//多个字段组合主键
                ;

            modelBuilder.Entity<UserProperty>()
                .ToTable("UserProperties")
                .HasKey(u => new{u.Key,u.UserId,u.Value});

            modelBuilder.Entity<BPFile>()
                .ToTable("UserBPFiles")
                .HasKey(u => u.Id);



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
