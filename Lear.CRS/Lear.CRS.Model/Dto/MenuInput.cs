﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 菜单参数
    /// </summary>
    public class MenuInput
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long Id { get; set; }
        public virtual long? Pid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public virtual string CnName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 菜单类型（字典 0目录 1菜单 2按钮）
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public virtual string Router { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public virtual string Component { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public virtual long? Mid { get; set; }

        /// <summary>
        /// 应用分类（应用编码）
        /// </summary>
        public virtual string Application { get; set; }

        /// <summary>
        /// 打开方式（字典 0无 1组件 2内链 3外链）
        /// </summary>
        public virtual string OpenType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 内链地址
        /// </summary>z
        public string Link { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class AddMenuInput : MenuInput
    {
        /// <summary>
        /// 菜单类型（字典 0目录 1菜单 2按钮）
        /// </summary>
        [Required(ErrorMessage = "菜单类型不能为空")]
        [JsonIgnore]
        public override int Type { get; set; }
    }

    public class DeleteMenuInput
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required(ErrorMessage = "菜单Id不能为空")]
        public long Id { get; set; }
    }

    public class UpdateMenuInput : MenuInput
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required(ErrorMessage = "菜单Id不能为空")]
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>DeleteMenuInput
        //[Required(ErrorMessage = "父级菜单Id不能为空")]
        [JsonIgnore]
        public override long? Pid { get; set; }
    }

    public class QueryMenuInput : PageInputBase
    {

    }

    public class ChangeAppMenuInput : MenuInput
    {
        /// <summary>
        /// 应用编码
        /// </summary>DeleteMenuInput
        [Required(ErrorMessage = "应用编码不能为空")]
        public override string Application { get; set; }
    }
}
