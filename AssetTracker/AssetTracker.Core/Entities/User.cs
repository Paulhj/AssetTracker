using Common;
using System.Collections.Generic;

namespace AssetTracker.Core.Entities
{
    public class User : IEntity
    {
        public User()
        {
            OrganizationUsers = new HashSet<OrganizationUser>();
        }

        public int Id { get; set; }
        public string NmFirst { get; set; }
        public string NmLast { get; set; }
        public string Email { get; set; }

        public ICollection<OrganizationUser> OrganizationUsers { get; set; }
    }
}
