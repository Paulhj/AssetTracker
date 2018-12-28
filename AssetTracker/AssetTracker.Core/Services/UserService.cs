using AssetTracker.Core.Entities;
using AssetTracker.Core.Repositories;
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Core.Services
{
    public class UserService : UnitOfWork<User>, IUserService
    {
        private IUserRepository _repository;

        public UserService(AssetTrackerContext context) : base(context)
        {
            _repository = new UserRepository(context);
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetByOrganizationId(
            int organizationId)
        {
            return await _repository.GetByOrganizationId
                (
                    organizationId
                );
        }

        public async Task<bool> Create(User item)
        {
            //Execute domain validation for the create page operation
            Validate(item, Operation.Create);

            _repository.Add(item);

            //Save the changes to data layer
            return await CompleteAsync();
        }

        public async Task<bool> Update(User item)
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
        protected override void ValidationRules(User item, Operation operation)
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
            if (string.IsNullOrWhiteSpace(item.NmFirst))
                AddBrokenRule("First name of the user cannot be blank");

            //Make sure the user has a last name.
            if (string.IsNullOrWhiteSpace(item.NmLast))
                AddBrokenRule("Last name of the user cannot be blank");
        }
    }
}
