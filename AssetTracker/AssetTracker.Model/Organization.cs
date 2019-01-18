using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Model
{
    public class Organization
    {
        public Organization()
        {
            Locations = new HashSet<Location>();
            Statuses = new HashSet<Status>();
            Types = new HashSet<Type>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public ICollection<Type> Types { get; set; }
    }
}
