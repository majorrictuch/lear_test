using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class PlanInput
    {
        /// <summary>
        /// Type
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PFromDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PToDate { get; set; }

      
        /// <summary>
        /// Plant
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PPlant { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String Department { get; set; }

        /// <summary>
        /// PLantFlage
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.Boolean PLantFlage { get; set; }

    }

    /// <summary>
    /// 新增参数
    /// </summary>
    public class AddPlanInput : PlanInput 
    {

        public System.Int32 PSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PSort { get; set; }
    }

    /// <summary>
    /// 更新
    /// </summary>

    public class UpdatePlanInput 
    {

        public System.Int32 PSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public System.String PFromDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public System.String PToDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PSort { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String PCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Department { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public System.String PType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PFormat { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String PContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PUpdateBy { get; set; }

    }

}
