using AssetTracker.Core.Entities;
using AssetTracker.Core.Repositories;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracker.Core.Services
{
    public class OrganizationService : UnitOfWork<Organization>, IOrganizationService
    {
        private IOrganizationRepository _repository;
        private IUserRepository _userRepository;

        public OrganizationService(AssetTrackerContext context) : base(context)
        {
            _repository = new OrganizationRepository(context);
            _userRepository = new UserRepository(context);
        }

        public Organization GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<IEnumerable<Organization>> GetByUserId(
            int userId)
        {
            return await _repository.GetByUserId
                (
                    userId
                );
        }

        public async Task<bool> Create(Organization item)
        {
            //Execute domain validation for the create page operation
            Validate(item, Operation.Create);

            _repository.Add(item);

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Update(Organization item)
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

        // Domain Validation - Put business rules here
        protected override void ValidationRules(Organization item, Operation operation)
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

            //Make sure the Organization has a name.
            if (string.IsNullOrWhiteSpace(item.Name))
                AddBrokenRule("Name of the organization cannot be blank");
        }

    }
}
