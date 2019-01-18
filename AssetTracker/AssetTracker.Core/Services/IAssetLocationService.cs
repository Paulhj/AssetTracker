using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Entities;

namespace AssetTracker.Core.Services
{
    public interface IAssetLocationService
    {
        Task<bool> Create(AssetLocation item);
        Task<bool> Delete(int id);
        Task<IEnumerable<AssetLocation>> GetByAssetId(int assetId);
        Task<AssetLocation> GetById(int assetId, int locationId, DateTime createDt);
        Task<bool> Update(AssetLocation item);
    }
}