using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public enum TenantTypeEnum
    {
        [Description("普通租户")]
        COMMON = 0,

        [Description("系统租户")]
        SYSTEM = 1,
    }
}
