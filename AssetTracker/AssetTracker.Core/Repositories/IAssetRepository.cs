using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;
using Common;

namespace AssetTracker.Core.Repositories
{
    internal interface IAssetRepository : IGenericRepository<Asset>
    {
        AssetTrackerContext AssetTrackerContext { get; }

        Task<IEnumerable<Asset>> GetByCriteria(int organizationId, AssetCriteria criteria);
        Asset GetById(int id);
        Task<Asset> GetByIdAsync(int id);
    }
}