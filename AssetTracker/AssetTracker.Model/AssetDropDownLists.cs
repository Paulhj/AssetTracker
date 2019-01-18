using System.Collections.Generic;

namespace AssetTracker.Model
{
    public class AssetDropDownLists
    {
        public AssetDropDownLists()
        {  }

        public AssetDropDownLists(
            IEnumerable<Location> locations,
            IEnumerable<Status> statuses,
            IEnumerable<Type> types)
        {
            Locations = locations;
            Statuses = statuses;
            Types = types;
        }

        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Type> Types { get; set; }
    }
}
