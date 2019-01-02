using AssetTracker.Core.Entities;
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
                var users = new List<User>
                {
                    new User { NmFirst = "Roger", NmLast="Williams", Email = "gopats@ne.com"},
                    new User { NmFirst = "John", NmLast="Routt", Email = "gowest@co.com"},
                    new User { NmFirst = "Davey", NmLast="Crockett", Email = "furs@usa.com"},
                };
                context.AddRange(users);
                context.SaveChanges();

                //Now add the Organization to  User Relationships
                var cotd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "Colorado Teardrops");

                var netd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "New England Teardrops");

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
            if (!context.Locations.Any())
            {
                var items = new List<Location>
                {
                    new Location { Name = "Main Warehouse" },
                    new Location { Name = "South Storage Unit" },
                    new Location { Name = "Showroom" }
                };
                context.AddRange(items);
                context.SaveChanges();
            }
        }

        public static void SeedStatus(AssetTrackerContext context)
        {
            if (!context.Statuses.Any())
            {
                var cotd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "Colorado Teardrops");

                var items = new List<Status>
                {
                    new Status { Name = "Received", OrganizationId = cotd.Id },
                    new Status { Name = "Available", OrganizationId = cotd.Id },
                    new Status { Name = "Hold", OrganizationId = cotd.Id },
                    new Status { Name = "Sold", OrganizationId = cotd.Id }
                };
                context.AddRange(items);
                context.SaveChanges();
            }
        }

        public static void SeedType(AssetTrackerContext context)
        {
            if (!context.Types.Any())
            {
                var cotd = context.Organizations
                    .Include(o => o.OrganizationUsers)
                    .Single(s => s.Name == "Colorado Teardrops");

                var items = new List<Entities.Type>
                {
                    new Entities.Type { Name = "Basedrop", OrganizationId = cotd.Id },
                    new Entities.Type { Name = "Canyonland", OrganizationId = cotd.Id },
                    new Entities.Type { Name = "Mount Massive", OrganizationId = cotd.Id },
                    new Entities.Type { Name = "The Summit", OrganizationId = cotd.Id },
                    new Entities.Type { Name = "Custom", OrganizationId = cotd.Id }
                };
                context.AddRange(items);
                context.SaveChanges();
            }
        }

        public static void SeedAssets(AssetTrackerContext context)
        {
            if (!context.Assets.Any())
            {
                var cotd = context.Organizations.Single(s => s.Name == "Colorado Teardrops");

                var netd = context.Organizations.Single(s => s.Name == "New England Teardrops");

                var baseType = context.Types.Single(s => s.Name == "Basedrop");
                var canyonType = context.Types.Single(s => s.Name == "Canyonland");
                var massiveType = context.Types.Single(s => s.Name == "Mount Massive");
                var summitType = context.Types.Single(s => s.Name == "The Summit");
                var customType = context.Types.Single(s => s.Name == "Custom");

                var recStatus = context.Statuses.Single(s => s.Name == "Received");
                var availStatus = context.Statuses.Single(s => s.Name == "Available");
                var holdStatus = context.Statuses.Single(s => s.Name == "Hold");
                var soldStatus = context.Statuses.Single(s => s.Name == "Sold");

                var mainLocation = context.Locations.Single(s => s.Name == "Main Warehouse");
                var southLocation = context.Locations.Single(s => s.Name == "South Storage Unit");
                var showLocation = context.Locations.Single(s => s.Name == "Showroom");

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
