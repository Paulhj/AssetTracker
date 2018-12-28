﻿using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Core.Entities
{
    public class Organization : IEntity
    {
        public Organization()
        {
            OrganizationUsers = new HashSet<OrganizationUser>();
            AssetOrganizations = new HashSet<AssetOrganization>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Organization Name cannot exceed 250 characters.")]
        public string Name { get; set; }

        public ICollection<OrganizationUser> OrganizationUsers { get; set; }
        public ICollection<AssetOrganization> AssetOrganizations { get; set; }
    }
}
