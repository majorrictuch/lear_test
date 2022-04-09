using Lear.CRS.Common.HttpContextUser;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 自定义实体基类
    /// </summary>
    public abstract class DEntityBase : PrimaryKeyEntity
    {




        public DEntityBase()
        {
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDescription = "创建时间")]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnDescription = "更新时间")]
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [MaxLength(20)]
        [SugarColumn(ColumnDescription = "创建者名称")]
        public virtual string CreateBy { get; set; }


        /// <summary>
        /// 修改者名称
        /// </summary>
        [MaxLength(20)]
        [SugarColumn(ColumnDescription = "修改者名称")]
        public virtual string UpdateBy { get; set; }

        public virtual void Create()
        {
            var userId = "001";
            var userName = "test1111";
            Id = YitIdHelper.NextId();
            CreateTime = DateTime.Now;
            CreateBy = userName;
        }

        public void Modify()
        {
            var userId = "";
            var userName = "";
            Id = YitIdHelper.NextId();
            UpdateTime = DateTime.Now;
            UpdateBy = userName;
        }


    }

    /// <summary>
    /// 递增主键实体基类
    /// </summary>
    public abstract class AutoIncrementEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(IsIdentity = true, ColumnDescription = "Id主键", IsPrimaryKey = true)] //通过特性设置主键和自增列 
        // 注意是在这里定义你的公共实体
        public virtual int Id { get; set; }
    }

    /// <summary>
    /// 主键实体基类
    /// </summary>
    public abstract class PrimaryKeyEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true)]
        // 注意是在这里定义你的公共实体
        public virtual long Id { get; set; }
    }
}
