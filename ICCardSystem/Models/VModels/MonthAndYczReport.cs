using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICCardSystem.Models.VModels
{
    public class MonthAndYczReport
    {
        public List<MonthReport> monthReportList { get; set; }
        public List<YchongzhangReport> yczList { get; set; }
    }
}