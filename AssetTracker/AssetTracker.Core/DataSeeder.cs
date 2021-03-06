﻿using AssetTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetTracker.Core
{
    public class DataSeeder
    {
        public static void SeedOrganizations(AssetTrackerContext context)
        {
            if (!context.Organizations.Any())
            {
                var users = new List<Organization>
                {
                    new Organization { Name = "Colorado Teardrops" },
                    new Organization { Name = "New England Teardrops" }
                };
                context.AddRange(users);
                context.SaveChanges();
            }
        }

        public static void SeedUsers(AssetTrackerContext context)
        {
            if (!context.Users.Any())
            {
                //Now add the Organization to  User Relationships
                var cotd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "Colorado Teardrops");

                var netd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "New England Teardrops");

                var users = new List<User>
                {
                    new User { NmFirst = "Roger", NmLast="Williams", Email = "gopats@ne.com", SelectedOrganizationId = cotd.Id},
                    new User { NmFirst = "John", NmLast="Routt", Email = "gowest@co.com", SelectedOrganizationId = cotd.Id},
                    new User { NmFirst = "Davey", NmLast="Crockett", Email = "furs@usa.com", SelectedOrganizationId = cotd.Id},
                };
                context.AddRange(users);
                context.SaveChanges();

                var roger = context.Users
                    .Single(s => s.NmFirst == "Roger");

                var john = context.Users
                    .Single(s => s.NmFirst == "John");

                var davey = context.Users
                    .Single(s => s.NmFirst == "Davey");

                cotd.OrganizationUsers.Add(new OrganizationUser
                {
                    User = john,
                    Organization = cotd
                });

                cotd.OrganizationUsers.Add(new OrganizationUser
                {
                    User = davey,
                    Organization = cotd
                });

                netd.OrganizationUsers.Add(new OrganizationUser
                {
                    User = roger,
                    Organization = netd
                });

                netd.OrganizationUsers.Add(new OrganizationUser
                {
                    User = davey,
                    Organization = netd
                });

                context.SaveChanges();
            }
        }

        public static void SeedLocations(AssetTrackerContext context)
        {
            var cotd = context.Organizations
                .Include(o => o.OrganizationUsers)
                .Include(l => l.Locations)
                .Single(s => s.Name == "Colorado Teardrops");

            if (!cotd.Locations.Any())
            {
                cotd.Locations.Add(new Location { Name = "Main Warehouse", OrganizationId = cotd.Id });
                cotd.Locations.Add(new Location { Name = "South Storage Unit", OrganizationId = cotd.Id });
                cotd.Locations.Add(new Location { Name = "Showroom", OrganizationId = cotd.Id });
                context.SaveChanges();
            }
        }

        public static void SeedStatus(AssetTrackerContext context)
        {
            var cotd = context.Organizations
                   .Include(o => o.OrganizationUsers)
                   .Include(s => s.Statuses)
                   .Single(s => s.Name == "Colorado Teardrops");

            if (!cotd.Statuses.Any())
            {
                cotd.Statuses.Add(new Status { Name = "Received", OrganizationId = cotd.Id });
                cotd.Statuses.Add(new Status { Name = "Available", OrganizationId = cotd.Id });
                cotd.Statuses.Add(new Status { Name = "Hold", OrganizationId = cotd.Id });
                cotd.Statuses.Add(new Status { Name = "Sold", OrganizationId = cotd.Id });
                context.SaveChanges();
            }
        }

        public static void SeedType(AssetTrackerContext context)
        {
            var cotd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Include(t => t.Types)
                    .Single(s => s.Name == "Colorado Teardrops");

            if (!cotd.Types.Any())
            {
                cotd.Types.Add(new Entities.Type { Name = "Basedrop", OrganizationId = cotd.Id });
                cotd.Types.Add(new Entities.Type { Name = "Canyonland", OrganizationId = cotd.Id });
                cotd.Types.Add(new Entities.Type { Name = "Mount Massive", OrganizationId = cotd.Id });
                cotd.Types.Add(new Entities.Type { Name = "The Summit", OrganizationId = cotd.Id });
                cotd.Types.Add(new Entities.Type { Name = "Custom", OrganizationId = cotd.Id });
                context.SaveChanges();
            }
        }

        public static void SeedAssets(AssetTrackerContext context)
        {
            if (!context.Assets.Any())
            {
                var cotd = context.Organizations.Single(s => s.Name == "Colorado Teardrops");

                var netd = context.Organizations.Single(s => s.Name == "New England Teardrops");

                var baseType = cotd.Types.Single(s => s.Name == "Basedrop");
                var canyonType = cotd.Types.Single(s => s.Name == "Canyonland");
                var massiveType = cotd.Types.Single(s => s.Name == "Mount Massive");
                var summitType = cotd.Types.Single(s => s.Name == "The Summit");
                var customType = cotd.Types.Single(s => s.Name == "Custom");

                var recStatus = cotd.Statuses.Single(s => s.Name == "Received");
                var availStatus = cotd.Statuses.Single(s => s.Name == "Available");
                var holdStatus = cotd.Statuses.Single(s => s.Name == "Hold");
                var soldStatus = cotd.Statuses.Single(s => s.Name == "Sold");

                var mainLocation = cotd.Locations.Single(s => s.Name == "Main Warehouse");
                var southLocation = cotd.Locations.Single(s => s.Name == "South Storage Unit");
                var showLocation = cotd.Locations.Single(s => s.Name == "Showroom");

                //First Asset
                var newasset = new Asset
                {
                    Tag = "23ERF34J",
                    Type = baseType,
                    Status = recStatus,
                    CreateDt = new DateTime(2018, 12, 3),
                    Description = "Red with rugged tire package"
                };

                context.Add(newasset);
                context.SaveChanges();

                var updateasset = context.Assets
                    .Include(l => l.AssetLocations)
                    .Include(o => o.AssetOrganizations)
                    .Single(s => s.Tag == "23ERF34J");

                updateasset.AssetLocations.Add(new AssetLocation()
                {
                    Location = mainLocation,
                    Asset = updateasset,
                    Note = "North wall",
                    CreateDt = new DateTime(2018, 12, 4)
                });

                updateasset.AssetOrganizations.Add(new AssetOrganization()
                {
                    Organization = cotd,
                    Asset = updateasset
                });

                context.SaveChanges();

                //Second Asset
                newasset = new Asset
                {
                    Tag = "98ERG36T",
                    Type = massiveType,
                    Status = availStatus,
                    CreateDt = new DateTime(2018, 10, 13),
                    Description = "Black with all these options..."
                };

                context.Add(newasset);
                context.SaveChanges();

                updateasset = context.Assets
                    .Include(l => l.AssetLocations)
                    .Include(o => o.AssetOrganizations)
                    .Single(s => s.Tag == "98ERG36T");

                updateasset.AssetLocations.Add(new AssetLocation()
                {
                    Location = showLocation,
                    Asset = updateasset,
                    Note = "Floor",
                    CreateDt = new DateTime(2018, 11, 24)
                });

                updateasset.AssetOrganizations.Add(new AssetOrganization()
                {
                    Organization = cotd,
                    Asset = updateasset
                });

                context.SaveChanges();
            }
        }
    }
}
