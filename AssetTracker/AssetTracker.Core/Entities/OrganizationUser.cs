using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Core.Entities
{
    public class OrganizationUser
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
