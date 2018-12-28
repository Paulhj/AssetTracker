using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class AssetRepository : GenericRepository<Asset>
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public AssetRepository(AssetTrackerContext context) : base(context)
        {

        }

        public Asset GetById(int id)
        {
            return AssetTrackerContext.Assets
                .Include(l => l.AssetLocations)
                .Include(o => o.AssetOrganizations)
                .FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<Asset>> GetByCriteria(
            int organizationId, AssetCriteria criteria)
        {

            IQueryable<Asset> query =
                 FindIf(true, o => o.AssetOrganizations.Select(i => i.OrganizationId).Contains(organizationId));
                 
                 //FindIf(mode == 1, c => c.ExpireDt >= DateTime.Today)
                //.FindIf(mode == 2, c => c.ExpireDt >= DateTime.Today.Subtract(TimeSpan.FromDays(30)))
                //.FindIf(mode == 3, c => c.ExpireDt >= DateTime.Today.Subtract(TimeSpan.FromDays(60)))
                //.FindIf(afterDt.HasValue, c => c.ExpireDt >= afterDt)
                //.FindIf(msgTypes.Count > 0, c => msgTypes.Contains(c.MsgTypeId))
                //.FindIf(msgAudiences.Count > 0, c => msgTypes.Contains(c.MsgAudienceId));

            return await query
                .Include(l => l.AssetLocations)
                .Include(o => o.AssetOrganizations)
                .ToListAsync();
        }
    }
}
