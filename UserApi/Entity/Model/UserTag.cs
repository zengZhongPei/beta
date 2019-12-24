using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Entity.Model
{
    public class UserTag
    {
        public int UserId { get; set; }

        [MaxLength(length:100)]
        public string Tag { get; set; }
    }
}
