using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model

{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogInput : PageInputBase
    {


        [JsonIgnore]
        public string FullName { get; set; }
        [JsonIgnore]
        public string EmpId { get; set; }
        [JsonIgnore]
        public string Account { get; set; }

        
    }

   
}
