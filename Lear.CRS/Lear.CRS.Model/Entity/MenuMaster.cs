using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuMaster
    {
        /// <summary>
        /// 
        /// </summary>
        public MenuMaster()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public System.Int64? Pid { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CnName { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 菜单类型（字典 0目录 1菜单 2按钮）
        /// </summary>
        public System.Int32 Type { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public System.String Router { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public System.String Component { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public System.Int32? Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Application { get; set; }

        /// <summary>
        /// 打开方式（字典 0无 1组件 2内链 3外链）
        /// </summary>
        public System.String OpenType { get; set; }

        /// <summary>
        /// 内链地址
        /// </summary>
        public System.String Link { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? Mid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateTime { get; set; }
    }
}