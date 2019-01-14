using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public AssetRepository(AssetTrackerContext context) : base(context)
        {

        }

        public Asset GetById(int id)
        {
            return AssetTrackerContext.Assets
                .Include(s => s.Status)
                .Include(t => t.Type)
                .Include(l => l.AssetLocations).ThenInclude(n => n.Location)
                .FirstOrDefault(f => f.Id == id);
        }

        public async Task<Asset> GetByIdAsync(int id)
        {
            return await AssetTrackerContext.Assets
                .Include(s => s.Status)
                .Include(t => t.Type)
                .Include(l => l.AssetLocations).ThenInclude(n => n.Location)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Asset>> GetByCriteria(
            int organizationId, AssetCriteria criteria)
        {

            //var test = 
            //    AssetTrackerContext.Assets
            //    .Include(s => s.Status)
            //    .Include(t => t.Type)
            //    .Include(l => l.AssetLocations).ThenInclude(n => n.Location)
            //    .Where(o => o.AssetOrganizations.Any(a => a.OrganizationId == 3))
            //    .ToList();

            IQueryable<Asset> query =
                FindIf(true, o => o.AssetOrganizations.Any(i => i.OrganizationId == organizationId));

                //FindIf(true, c => c.CreateDt < DateTime.Today);
                //.FindIf(mode == 2, c => c.ExpireDt >= DateTime.Today.Subtract(TimeSpan.FromDays(30)))
                //.FindIf(mode == 3, c => c.ExpireDt >= DateTime.Today.Subtract(TimeSpan.FromDays(60)))
                //.FindIf(afterDt.HasValue, c => c.ExpireDt >= afterDt)
                //.FindIf(msgTypes.Count > 0, c => msgTypes.Contains(c.MsgTypeId))
                //.FindIf(msgAudiences.Count > 0, c => msgTypes.Contains(c.MsgAudienceId));

            return await query
                .Include(s => s.Status)
                .Include(t => t.Type)
                .Include(l => l.AssetLocations).ThenInclude(n => n.Location)
                .ToListAsync();
        }
    }
}
