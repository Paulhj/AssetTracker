using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class StatusRepository : GenericRepository<Status>
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public StatusRepository(AssetTrackerContext context) : base(context)
        {

        }

        public Status GetById(int id)
        {
            return AssetTrackerContext.Statuses
                .FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<Status>> GetByOrganizationId(int organizationId)
        {
            IQueryable<Status> query =
                 FindIf(true, u => u.OrganizationId == organizationId);

            return await query
                .ToListAsync();
        }
    }
}
