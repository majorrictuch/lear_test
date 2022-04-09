using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Dto
{
    /// <summary>
    /// 版本记录
    /// </summary>
    public class VersionHistoryInput
    {
        /// <summary>
        /// 
        /// </summary>
        public System.String Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RevisionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Developer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RevisionNotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Tickets { get; set; }



    }

    public class AddVersionHistoryInput : VersionHistoryInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Version { get; set; }


    }

    public class DeleteVersionHistoryInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        public long Id { get; set; }
    }

    public class UpdateVersionHistoryInput : VersionHistoryInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        public long Id { get; set; }
    }
}
