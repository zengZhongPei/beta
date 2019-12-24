using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Entity.Model
{
    public class UserProperty
    {
        public int UserId { get; set; }

        [MaxLength(length: 100)]
        public string Key { get; set; }

        [MaxLength(length: 100)]
        public string Value { get; set; }

        public string Text { get; set; }
    }
}
