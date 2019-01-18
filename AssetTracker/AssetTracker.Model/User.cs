using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.Model
{
    public class User
    {
        public int Id { get; set; }
        public string NmFirst { get; set; }
        public string NmLast { get; set; }
        public string Email { get; set; }
        public int SelectedOrganizationId { get; set; }
    }
}
