using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;
using AssetTracker.Core.Repositories;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Core.Services
{
    public class AssetLocationService : UnitOfWork<AssetLocation>, IAssetLocationService
    {
        private IAssetRepository _repository;

        public AssetLocationService(AssetTrackerContext context) : base(context)
        {
            _repository = new AssetRepository(context);
        }

        #region Authorization Support Methods

        #endregion

        #region Fetch Methods

        public async Task<AssetLocation> GetById(int assetId, int locationId, DateTime createDt)
        {
            var asset = await _repository.GetByIdAsync(assetId);

            return asset.AssetLocations.Where(i =>
                i.AssetId == assetId &&
                i.LocationId == locationId &&
                i.CreateDt == createDt).FirstOrDefault();
        }

        public async Task<IEnumerable<AssetLocation>> GetByAssetId(int assetId)
        {
            var asset = await _repository.GetByIdAsync(assetId);
            return asset.AssetLocations.OrderByDescending(d => d.CreateDt);
        }

        #endregion
        
        #region Create / Update / Delete Methods

        public async Task<bool> Create(AssetLocation item)
        {
            //Execute domain validation for the create page operation
            Validate(item, Operation.Create);

            //Get the asset to add new location to
            var asset = _repository.GetById(item.AssetId);

            //Get the current location and set the transfer date
            var location = asset.AssetLocations
                .OrderByDescending(i => i.CreateDt)
                .FirstOrDefault();

            location.TransferDt = item.CreateDt;

            //add the new location to the asset
            asset.AssetLocations.Add(new AssetLocation()
            {
                LocationId = item.LocationId,
                Note = item.Note,
                CreateDt = item.CreateDt
            });

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Update(AssetLocation item)
        {
            //Execute domain validation for the update page operation
            Validate(item, Operation.Update);

            //Get the asset to add new location to
            var asset = _repository.GetById(item.AssetId);

            //Get the current location and set the transfer date
            var location = asset.AssetLocations
                .OrderByDescending(i => i.CreateDt)
                .FirstOrDefault();

            location.Note = item.Note;
            location.TransferDt = item.TransferDt;

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            //var item = _repository.GetById(id);

            //Execute domain validation for the update page operation
            //Validate(item, Operation.Delete);

            //_repository.Remove(item);
            //Save the changes to data layer
            return await CompleteAsync();
        }

        #endregion

        #region Validation Methods

        // Domain Validation - Put business rules here
        protected override void ValidationRules(AssetLocation item, Operation operation)
        {
            // create domain validation logic
            if (operation == Operation.Create)
            {
                //if (item.StartDt < DateTime.Today)
                //    AddBrokenRule("Start Date cannot be before today.");

            }

            // update domain validation logic
            if (operation == Operation.Update)
            {
            }

            // delete domain validation logic
            if (operation == Operation.Delete)
            {
            }
        }

        #endregion
    }
}
