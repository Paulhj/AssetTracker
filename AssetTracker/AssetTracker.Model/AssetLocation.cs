using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Model
{
    public class AssetLocation
    {
        public int LocationId { get; set; }
        public string LocationNm { get; set; }
        public string Note { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime TransferDt { get; set; }
    }
}
