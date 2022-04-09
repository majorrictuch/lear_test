using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Permission
{
    public class RoleModulePermission
    {

        public long RoleId { get; set; }
        public long ModuleId { get; set; }
        public long PermissionId { get; set; }



        public Role Role { get; set; }
        public Modules Module { get; set; }
        public Permission Permission { get; set; }

    }
}
