using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public class ResourceInput
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public string ResourceName { get; set; }
        [JsonIgnore]
        public string ResourceDesc { get; set; }
        [JsonIgnore]
        public int? Sort { get; set; }
        [JsonIgnore]
        public long? ParentId { get; set; }
        [JsonIgnore]
        public long ManagerId { get; set; }
    }

    public class UpdateResourceInput : ResourceInput
    { 
    
    }
    public class QueryResourceInput : PageInputBase
    {

    }

    public class AddResourceInput 
    {
        [JsonIgnore]
        public string ResourceName { get; set; }
        [JsonIgnore]
        public string ResourceDesc { get; set; }
        [JsonIgnore]
        public int? Sort { get; set; }
        [JsonIgnore]
        public long? ParentId { get; set; }
        [JsonIgnore]
        public long ManagerId { get; set; }
    }

}
