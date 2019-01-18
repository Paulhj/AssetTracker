using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class TypeRepository : GenericRepository<Entities.Type>
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public TypeRepository(AssetTrackerContext context) : base(context)
        {

        }

        public Entities.Type GetById(int id)
        {
            return null; // AssetTrackerContext.Types
                //.FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<Entities.Type>> GetByOrganizationId(int organizationId)
        {
            IQueryable<Entities.Type> query =
                 FindIf(true, u => u.OrganizationId == organizationId);

            return await query
                .ToListAsync();
        }
    }
}
