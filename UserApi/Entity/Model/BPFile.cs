using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Entity.Model
{
    public class BPFile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 上传文件的原始地址
        /// </summary>
        public string OriginFilePath { get; set; }

        /// <summary>
        /// 格式化转换后的地址
        /// </summary>
        public string FormatFilePath { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }
}
