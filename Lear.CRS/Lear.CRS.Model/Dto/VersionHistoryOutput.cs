using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Dto
{


    public class VersionHistoryOutput
    {
        /// <summary>
        /// 
        /// </summary>

        public System.Int64 Id { get; set; }

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
}
