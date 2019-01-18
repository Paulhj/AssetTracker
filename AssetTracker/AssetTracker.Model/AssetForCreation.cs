using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Model
{
    public class AssetForCreation : AssetDropDownLists
    {
        public AssetForCreation()
        {  }

        public AssetForCreation(
            IEnumerable<Location> locations,
            IEnumerable<Status> statuses,
            IEnumerable<Type> types) 
            : base(locations, statuses, types)
        {

        }

        //Asset Properties
        [Required]
        [MaxLength(250)]
        public string Tag { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int TypeId { get; set; }

        //Asset Location Properties
        [Required]
        public int LocationId { get; set; }
        public string Note { get; set; }
    }
}
