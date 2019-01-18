using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public OrganizationRepository(AssetTrackerContext context) : base(context)
        {

        }

        public async Task<Organization> GetById(int id)
        {
            return await AssetTrackerContext.Organizations
                .Include(u => u.OrganizationUsers)
                .Include(l => l.Locations)
                .Include(s => s.Statuses)
                .Include(t => t.Types)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Organization>> GetByUserId(int userId)
        {
            IQueryable<Organization> query =
                 FindIf(true, u => u.OrganizationUsers.Select(i => i.UserId).Contains(userId));

            return await query
                .Include(u => u.OrganizationUsers)
                .ToListAsync();
        }

    }
}
