using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public class SourceGroupOutput : SourceGroupInput
    {


        public List<long> RecourceIds { get; set; }
        public List<long> UserIds { get; set; }

    }


}
