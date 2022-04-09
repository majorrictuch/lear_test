using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 角色绑定用户
    /// </summary>


    public class BindToUserInput
    {

        public long UserId { get; set; }
        public List<long> RoleId { get; set; }

    }
    /// <summary>
    /// 用户绑定角色
    /// </summary>
    public class BindToRoleInput
    {

        public long RoleId { get; set; }
        public List<long> UserId { get; set; }

    }


    public class GroupToUserInput
    {

        public long UserId { get; set; }
        public List<long> GroupId { get; set; }

    }
    /// <summary>
    /// 用户绑定角色
    /// </summary>
    public class UserToGroupInput
    {

        public long GroupId { get; set; }
        public List<long> UserId { get; set; }

    }
}
