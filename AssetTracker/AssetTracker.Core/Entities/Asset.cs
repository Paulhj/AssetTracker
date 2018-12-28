using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Core.Entities
{
    public class Asset : IEntity
    {
        public Asset()
        {
            AssetLocations = new HashSet<AssetLocation>();
            AssetOrganizations = new HashSet<AssetOrganization>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Asset Tag Cannot Exceed 250 characters.")]
        public string Tag { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        public DateTime CreateDt { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Required]
        public int TypeId { get; set; }
        public Type Type { get; set; }

        public ICollection<AssetLocation> AssetLocations { get; set; }
        public ICollection<AssetOrganization> AssetOrganizations { get; set; }
    }
}
