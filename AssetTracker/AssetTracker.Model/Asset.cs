using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Model
{
    public class Asset
    {
        public int AssetId { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        public DateTime CreateDt { get; set; }

        public int StatusId { get; set; }
        public string StatusNm { get; set; }

        public int TypeId { get; set; }
        public string TypeNm { get; set; }

        public AssetLocation CurrentLocation { get; set; }
    }
}
