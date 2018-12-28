using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Core.Entities
{
    public class AssetOrganization
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
