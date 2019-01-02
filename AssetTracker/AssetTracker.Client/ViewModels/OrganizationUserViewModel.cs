using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Client.Models
{
    public class OrganizationViewModel
    {
        public IEnumerable<Models.User> Users { get; private set; }
            = new List<Models.User>();

        public OrganizationViewModel(List<Models.User> users)
        {
            Users = users;
        }

    }

    public class UserViewModel
    {
        public IEnumerable<Organization> Organizations { get; private set; }
            = new List<Organization>();

        public UserViewModel(List<Organization> organizations)
        {
            Organizations = organizations;
        }

    }

    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class User
    { 
        public int Id { get; set; }
        public string NmFirst { get; set; }
        public string NmLast { get; set; }
        public string Email { get; set; }
    }
}
