using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Entities;
using Common;

namespace AssetTracker.Core.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        AssetTrackerContext AssetTrackerContext { get; }

        Organization GetById(int id);
        Task<IEnumerable<Organization>> GetByUserId(int userId);
    }
}