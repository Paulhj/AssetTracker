using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Model
{
    public class AssetForCreation
    {
        //Asset Properties
        public string Tag { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        //Asset Location Properties
        public int LocationId { get; set; }
        public string Note { get; set; }
    }
}
