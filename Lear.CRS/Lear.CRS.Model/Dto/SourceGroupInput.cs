using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public class SourceGroupInput
    {
        public long Id { get; set; }
        public string Name { get; set; }


        public List<long> ResourceIds { get; set; }
        public List<long> UserIds { get; set; }



    }

    public class UpdateSourceGroupInput : SourceGroupInput
    {

    }
    public class QuerySourceGroupInput : PageInputBase
    {
        [JsonIgnore]
        public string Name { get; set; }
    }

    public class AddSourceGroupInput
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public List<long> ResourceIds { get; set; }
        [JsonIgnore]
        public List<long> UserIds { get; set; }
    }

}
