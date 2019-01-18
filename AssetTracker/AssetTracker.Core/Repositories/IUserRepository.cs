using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Entities;
using Common;

namespace AssetTracker.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        AssetTrackerContext AssetTrackerContext { get; }

        User GetById(int id);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByOrganizationId(int organizationId);
    }
}