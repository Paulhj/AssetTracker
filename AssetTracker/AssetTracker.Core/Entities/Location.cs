using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Core.Entities
{
    public class Location : IEntity
    {
        public Location()
        {
            AssetLocations = new HashSet<AssetLocation>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Location Name cannot exceed 250 characters.")]
        public string Name { get; set; }

        public ICollection<AssetLocation> AssetLocations { get; set; }
    }
}
