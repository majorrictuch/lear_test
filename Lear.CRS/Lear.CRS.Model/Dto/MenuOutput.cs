using Lear.CRS.Common;
using System.Collections;
using System.Collections.Generic;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 菜单树（列表形式）
    /// </summary>
    public class MenuOutput : MenuInput
    {
     
        public long Id { get; set; }

       
        public string ModuleName{ get; set; }
       
    }
    public class MenuCurrentOutput : MenuInput
    {

        public long Id { get; set; }


        public string ModuleName { get; set; }

    }

}
