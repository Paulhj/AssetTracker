using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Core.Entities
{
    public class Status : IEntity
    {
        public Status()
        {
            Assets = new HashSet<Asset>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Status Name cannot exceed 150 characters.")]
        public string Name { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
