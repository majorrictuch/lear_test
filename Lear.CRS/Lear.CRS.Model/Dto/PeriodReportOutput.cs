using System;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class PeriodReportOutput
    {
        public int PSeq { get; set; }
        public int PSort { get; set; }

        public String PDesc { get; set; }

        public String PContent { get; set; }

        public String PContentPlan { get; set; }

        public String PRemark { get; set; }

        public String PFormat { get; set; }

        public String PCompare { get; set; }

        public string ZPeriod { get; set; }
        public string ZContent { get; set; }
        public string ZContentPlan { get; set; }
        public string LContent { get; set; }
        public bool IShow { get; set; }



    }
}
