using Microsoft.EntityFrameworkCore;
using UserApi.Model;

namespace UserApi.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
