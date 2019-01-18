using AssetTracker.Core.Criteria;
using AssetTracker.Core.Entities;
using AssetTracker.Core.Repositories;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracker.Core.Services
{
    public class AssetService : UnitOfWork<Asset>, IAssetService
    {
        private IAssetRepository _repository;

        public AssetService(AssetTrackerContext context) : base(context)
        {
            _repository = new AssetRepository(context);
        }

        #region Authorization Support Methods

        public bool IsAssetOwner(string assetId, int organizationId)
        {
            //Implement later.  Return true for now for Authorization test.
            return true;
        }

        #endregion

        #region Fetch Methods

        public async Task<Asset> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Asset>> GetByCriteria(
            int organizationId, AssetCriteria criteria)
        {
            return await _repository.GetByCriteria
                (
                    organizationId,
                    criteria
                );
        }

        #endregion

        #region Create / Update / Delete Methods

        public async Task<bool> Create(Asset item)
        {
            //Execute domain validation for the create page operation
            Validate(item, Operation.Create);

            _repository.Add(item);

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Update(Asset item)
        {
            //Execute domain validation for the update page operation
            Validate(item, Operation.Update);

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var item = _repository.GetById(id);

            //Execute domain validation for the update page operation
            Validate(item, Operation.Delete);

            _repository.Remove(item);
            //Save the changes to data layer
            return await CompleteAsync();
        }

        #endregion

        #region Validation Methods

        // Domain Validation - Put business rules here
        protected override void ValidationRules(Asset item, Operation operation)
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

            //Make sure the user has a first name.
            if (string.IsNullOrWhiteSpace(item.Tag))
                AddBrokenRule("Asset tag cannot be blank");

            //Check to see if tag is already in the database.


            
        }

        #endregion
    }
}
