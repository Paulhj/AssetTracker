using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;

namespace AssetTracker.Core.Services
{
    public interface IAssetService
    {
        Task<bool> Create(Asset item);
        Task<bool> Delete(int id);
        Task<IEnumerable<Asset>> GetByCriteria(int organizationId, AssetCriteria criteria);
        Asset GetById(int id);
        Task<bool> Update(Asset item);
    }
}