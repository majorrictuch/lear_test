using System;
using System.Collections.Generic;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 登录用户角色参数
    /// </summary>
    public class RoleOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

       
        public int Sort { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Enabled { get; set; }


        public List<long> UserIds { get; set; }
        public List<long> MenuIds { get; set; }

    }
}
