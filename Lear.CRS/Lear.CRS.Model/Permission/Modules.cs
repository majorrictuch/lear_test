using SqlSugar;
using System;

namespace Lear.CRS.Model.Permission
{
    /// <summary>
    /// 接口API地址信息表
    /// </summary>
    public class Modules : ModulesRoot<int>
    {
        public Modules()
        {
          
        }


        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Name { get; set; }
       
        /// <summary>
        /// Action名称
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Action { get; set; }
      
        /// <summary>
        /// /描述
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Description { get; set; }
      
       
      
    }
}
