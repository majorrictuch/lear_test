using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Dto
{
  
    public class AssignInput
    {
        [JsonIgnore]
        public List<long> MenuIds { get; set; }
        [JsonIgnore]
        public long RoleId { get; set; }
    }
    public class UserBindInput
    {
        [JsonIgnore]
        public long UserId { get; set; }
        [JsonIgnore]
        public List<long> GroupIds { get; set; }
        [JsonIgnore]
        public List<long> RoleIds { get; set; }
    }
}
