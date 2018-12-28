using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Core.Criteria
{
    public class AssetCriteria
    {
        public string Tag { get; set; }
        public DateTime? CreateDt { get; set; }
        public int? StatusId { get; set; }
        public int? TypeId { get; set; }
        public int? LocationId { get; set; }
    }
}
