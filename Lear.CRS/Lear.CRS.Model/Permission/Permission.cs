using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Permission
{
    public partial class Permission 
    {

    
        public long Id { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 是否按钮
        /// </summary>
        public System.Boolean IsButton { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public System.Boolean IsHide { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>

        public string LinkUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IskeepAlive { get; set; }

      

        /// <summary>
        /// 显示顺序
        /// </summary>
        public System.Int32? Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public System.String Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public System.Boolean? Enabled { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public System.Int64 Pid { get; set; }

        /// <summary>
        /// 模块id
        /// </summary>
        public System.Int64 Mid { get; set; }

       
    }
}
