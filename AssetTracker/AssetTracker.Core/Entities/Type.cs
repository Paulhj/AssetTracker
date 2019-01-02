using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Core.Entities
{
    public class Type : IEntity
    {
        public Type()
        {
            Assets = new HashSet<Asset>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Type Name cannot exceed 150 characters.")]
        public string Name { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
