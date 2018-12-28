using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Core.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public AssetTrackerContext AssetTrackerContext { get { return (AssetTrackerContext)_context; } }

        public UserRepository(AssetTrackerContext context) : base(context)
        {

        }

        public User GetById(int id)
        {
            return AssetTrackerContext.Users
                .Include(u => u.OrganizationUsers)
                .FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<User>> GetByOrganizationId(int organizationId)
        {
            IQueryable<User> query =
                 FindIf(true, u => u.OrganizationUsers.Select(i => i.OrganizationId).Contains(organizationId));

            return await query
                .Include(u => u.OrganizationUsers)
                .ToListAsync();
        }
    }
}
